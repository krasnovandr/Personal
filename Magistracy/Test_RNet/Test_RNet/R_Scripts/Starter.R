function(folderName,directoryPath) {
  
  library(tm) # Framework for text mining.
  library(cluster)
  # library(hclust)
  
   # cname <- file.path("E:/Users/Andrei/Desktop/R_Test", "", "")

  cname <- file.path(folderName, "", "")
  # fil<-c("1.txt", "2.txt", "3.txt") 
  docs <- Corpus(DirSource(cname))
  meta(docs[[1]])
  # filtered<-tm_filter(docs[1:8], FUN = function(x) meta(x)[["id"]] == fil)
  # filtered
  docs
  docs <- tm_map(docs, content_transformer(tolower))
  docs <- tm_map(docs, removeNumbers)
  docs <- tm_map(docs, removePunctuation)
  docs <- tm_map(docs, removeWords, stopwords("english"))
  docs <- tm_map(docs, stripWhitespace)
  docs <- tm_map(docs, stemDocument)

  
  dtm <- DocumentTermMatrix(docs)

   scriptPath <- file.path(directoryPath,"WordCloud.R")
   cloud <- dget(scriptPath)
   cloud(dtm,folderName)
  
  
   scriptPath <- file.path(directoryPath,"PlaneClustering.R")
   clustering <- dget(scriptPath)
   clustering(dtm,folderName)


  # docs <- Corpus(DirSource(cname))
  # 
  # 
  # docs <- tm_map(docs, content_transformer(tolower))
  # docs <- tm_map(docs, removeNumbers)
  # docs <- tm_map(docs, removePunctuation)
  # docs <- tm_map(docs, removeWords, stopwords("english"))
  # docs <- tm_map(docs, stripWhitespace)
  # docs <- tm_map(docs, stemDocument)
  # 
  # dtm <- DocumentTermMatrix(docs)
  # m <- as.matrix(dtm)
  # library(proxy)
  # d <- dist(m, method="cosine")
  # hc <- hclust(d, method="average")
  # plot(hc)
  # 
  # 

}
