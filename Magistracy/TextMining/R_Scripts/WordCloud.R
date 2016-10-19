function (dtm,resutDirectory) {
  library(wordcloud)
  
  freq <- colSums(as.matrix(dtm))
  freq <- sort(colSums(as.matrix(dtm)), decreasing=TRUE)
  
  set.seed(142)

  fullPath <- file.path(resutDirectory,"wordcloud.png")
  png(fullPath)
  res<-wordcloud(names(freq), freq, max.words=100, scale=c(5, .1), colors=brewer.pal(6, "Dark2"))
  dev.off()
}
