function (directory) {
  library(tm) # Framework for text mining.
  library(cluster)
  library(proxy)
   # cname <- file.path("D:/GitReps/Personal/Magistracy/Test_RNet/Test_RNet/bin/Debug/c1e45218-79da-4df4-b886-8ec279a35d34/3", "", "")
 cname <- file.path(directory, "", "")

   docs <- Corpus(DirSource(cname))
  
   # filtered<-tm_filter(docs[1:length(dir(cname))], FUN = function(x) meta(x)[["id"]] == greetings)
  
   docs <- tm_map(docs, content_transformer(tolower))
   docs <- tm_map(docs, removeNumbers)
   docs <- tm_map(docs, removePunctuation)
   docs <- tm_map(docs, removeWords, stopwords("english"))
   docs <- tm_map(docs, stripWhitespace)
   docs <- tm_map(docs, stemDocument)
  
  
   dtm <- DocumentTermMatrix(docs)
  
   m <- as.matrix(dtm)
  
   # library(proxy)
  
   ### this is going to take 4-ever (O(n^2))
   d <- dist(m, method="cosine")

   hc <- hclust(d, method="average")
   directoryPath <-normalizePath(directory)
   fullPath <- file.path(directoryPath,"HierarchicalClustering.png")
   png(fullPath)
     plot(hc)
    dev.off()
 
  frame <- as.data.frame(hc[1])
 frame
}
