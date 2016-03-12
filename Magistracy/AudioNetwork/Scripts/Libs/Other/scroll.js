/**
* Created by netBeans.
* User: abaddon
* Date: 12.04.14
* Time: 13:10
* Description: Бесконечный скролл
*/
var scroll = angular.module("infScroll", []);
/*
* Инициализация прокрутки
*/
scroll.directive("scrollInit", ["$infScroll", "$rootScope", function ($infScroll, $rootScope) {
    return {
        link: function (scope, elem, attr) {
            var timestamp = $infScroll.timestamp;

            //Запускаем
            var stop = scope.$watch("startScroll", function (value) {
                if (value) {
                    value.run(scope, elem);
                    stop();
                }
            });
        }
    };
} ]);
/*
* Показывает preloder
*/
scroll.directive("scrollLoader", [function () {
    return {
        replace: true,
        template: "<div class='loader' ng-show='showLoader' style='background: url({{gifPath}}) 50% 50% no-repeat'></div>",
        link: function (scope, elem, attr) {
            scope.$emit("loading:scroll", scope);
        }
    };
} ]);
/*
* Интерпритирует теги и выражение angular-ра
*/
scroll.directive("scrollParse", ['$sce', '$compile', function ($sce, $compile) {
    var content = null;
    return {
        compile: function compile(tElement, tAttrs, transclude) {
            return {
                pre: function preLink(scope, elem, attr) {
                    var html = scope.$eval(attr.scrollParse);
                    content = angular.element('<div></div>').append(html).contents();
                },
                post: function postLink(scope, elem, attr) {
                    elem.append(content);
                    $compile(content)(scope);
                }
            };
        }
    };
} ]);
//интерпритирует html- теги
scroll.filter("html", ['$sce', function ($sce) {
    return function (input) {
        return $sce.trustAsHtml(input);
    };
} ]);
/*
* Конструктор нашей функции
*/
scroll.factory("$infScroll", ['$rootScope', '$window', '$document', '$http', '$compile', '$location', '$templateCache', function (
    $rootScope,
    $window,
    $document,
    $http,
    $compile,
    $location,
    $templateCache
    ) {
    var W = angular.element($window), scrollTop, offset, accept, dH, wH;

    var Scroll = function (options) {
        if (!(this instanceof Scroll)) {
            return new Scroll(options);
        }
        //Сброс значений на дефолт
        scrollTop = 0;
        offset = 0;
        accept = true; dH = 0;
        wH = 0;

        //Настройки
        angular.extend(this, {
            external: true,
            button: false,
            url: null,
            gifPath: null,
            indentToScroll: 200,
            template: null,
            limit: 5,
            offset: 0,
            scope: $rootScope,
            timestamp: Date.now(),
            method: "post",
            headers: null,
            alias: 'results',
            responseType: "json",
            heightWatch: null,
            accept: true,
            userControll: false,
            locScope: null,
            pushState: false
        }, options);

        limit = this.limit;
        //Запуск скролла
        this.run = function (scope, elem) {
            if (!this.locScope) this.locScope = scope;
            this.elem = elem;
            this.locScope[this.alias] = [];

            if (!this.template) {
                if (!this.button) {
                    this.regScrollEvent();
                } else {
                    this.getTheData();
                }
            } else {
                this.loadTemplate(function () {
                    if (!this.button) {
                        this.regScrollEvent();
                    } else {
                        this.getTheData();
                    }
                } .bind(this));
            }
        };
        //Прелодер
        this.scope.$on("loading:scroll", function (e, scope) {
            scope.gifPath = this.gifPath;
        } .bind(this));

        this.flag = 'startScroll';
        this.locScope[this.flag] = this;
    };

    Scroll.prototype = {
        bind: function (event, handler) {
            var name = event + ":" + this.timestamp;
            this.scope.$on(name, handler.bind(this));
        },
        trigger: function (event) {
            arguments[0] = event + ":" + this.timestamp;
            this.scope.$broadcast.apply(this.scope, arguments);
        },
        /*
        * Подгружает шаблон
        */
        loadTemplate: function (callback) {
            $http({ method: 'get', cache: $templateCache, url: this.template }).success(function (template, status) {
                this.elem.append(template);
                content = this.elem.contents();
                $compile(content)(this.locScope);
                callback();
            } .bind(this)).error(function (data, status) {
                this.trigger("scrollError", data, status);
            });
        },
        //Преобразует объект, массив или массив объектов в строку для запроса
        toParam: function (data, prefix) {
            var requestString = [], value = null, i = 0, prefix = prefix || 0;
            if (angular.isArray(data)) {//если массив
                var ln = data.length;
                while (ln--) {
                    value = data[ln];
                    if (angular.isObject(value)) {
                        requestString.push(this.toParam(value, prefix));
                    } else {
                        requestString.push('param_' + prefix + '=' + encodeURIComponent(value));
                    }
                    prefix++;
                }
            } else {//если объект
                for (var j in data) {
                    value = data[j];
                    if (angular.isObject(value)) {
                        requestString.push(this.toParam(value, prefix));
                    } else {
                        requestString.push(j + '=' + encodeURIComponent(value));
                    }
                    i++;
                }
            }
            return requestString.join("&");
        },
        regScrollEvent: function () {
            var win = null, doc = null, big = false, small = true;
            if (this.external) {
                win = W;
                doc = $document[0].documentElement;
                dH = doc.clientHeight;
            } else {
                dH = this.elem[0].clientHeight;
                win = this.elem;
                doc = this.elem[0];
            }
            win.unbind('scroll');
            win.bind('scroll', function (e) {
                scrollTop = win[0].pageYOffset || doc.scrollTop;
                wH = Math.max(doc.scrollHeight, dH); //Длина с учетом подгруженных элементов

                if (scrollTop + this.indentToScroll >= wH - dH && this.accept) {
                    //Новая порция
                    this.getTheData();
                }

                if (this.heightWatch) {
                    if (scrollTop > this.heightWatch && !big) {
                        this.trigger('after:deadline');
                        big = true;
                        small = false;
                    }

                    if (scrollTop < this.heightWatch && !small) {
                        this.trigger('prev:deadline');
                        big = false;
                        small = true;
                    }
                }

            } .bind(this));
            this.getTheData();
        },
        prepareResult: function (data) {
            switch (this.responseType) {
                case 'json':
                    if (data instanceof Array) {
                        var ln = data.length, i = 0;
                        while (i < ln) {
                            this.locScope[this.alias].push(data[i]);
                            i++;
                        }
                        if (ln < this.limit) {
                            this.accept = false;
                        } else {
                            this.accept = true;
                        }
                    } else {
                        this.locScope[this.alias].push(data);
                        if (data.length < this.limit) {
                            this.accept = false;
                        } else {
                            this.accept = true;
                        }
                    }
                    break;
                case 'html':
                    var content = angular.element('<div></div>').append(data).contents(), ln = content.length;
                    this.elem.append(content)
                    $compile(content)(this.locScope);
                    if (ln < this.limit) {
                        this.accept = false;
                    } else {
                        this.accept = true;
                    }
                    break;
            }
        },
        /*
        * Делает запрос на получение очередной порции данных
        */
        getTheData: function () {
            this.accept = false;
            var scope = this.locScope, method = this.method.toUpperCase(),
                settings = {
                    method: this.method,
                    url: this.url,
                    headers: this.headers || { 'Content-Type': 'application/x-www-form-urlencoded; charset=utf-8' },
                    responseType: this.responseType
                }, dop = {};
            //Показываем preloder
            scope.showLoader = true;
            this.trigger("beforeScroll");
            //Берет данные из адресной строки и цепляет его к запросу
            if (this.pushState) {
                var searchString = $location.search(), searchStringKeys = Object.keys(searchString);
                if (searchStringKeys.length) {
                    angular.forEach(searchString, function (v, k) {
                        if (!this.data[k]) {
                            this.data[k] = v;
                        }
                    } .bind(this));
                }
            }

            switch (method) {
                case 'POST':
                    this.data.limit = limit;
                    this.data.offset = offset;
                    dop = {
                        data: this.data,
                        transformRequest: function (data) {
                            return this.toParam(data);
                        } .bind(this)
                    };
                    break;
                case 'GET':
                    this.data.limit = limit;
                    this.data.offset = offset;
                    dop = {
                        params: this.data
                    };
                    break;
            };
            settings = angular.extend(settings, dop);
            $http(settings).success(function (data, status) {
                if (!this.userControll) {
                    this.prepareResult(data);
                } else {
                    this.trigger("userDataScrollControl", data, status);
                }
                scope.showLoader = false;
                offset += this.limit;
                this.trigger("afterScroll", data, status);
                //Нужно ли изменять url
                if (this.pushState) {
                    //Перед изменением url
                    this.trigger("beforePushState", data, status);
                    var searchString = $location.search();
                    angular.forEach(this.data, function (v, k) {
                        searchString[k] = v;
                    });
                    $location.search(searchString);
                }
            } .bind(this)).error(function (data, status) {
                this.trigger("scrollError", data, status);
            });
        },
        reset: function () {
            switch (this.responseType) {
                case 'json':
                    this.locScope[this.alias] = [];
                    break;
                case 'html':
                    this.elem.html('');
                    break;
            }
            offset = 0;

            if (!this.pushState) {
                $location.search({});
            }

            this.getTheData();
        },
        //Дополнительные данные для запроса
        data: {}
    };

    return {
        init: function (options) {
            return Scroll(options);
        }
    };
} ]);