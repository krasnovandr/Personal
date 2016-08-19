function (directory,files) {
  library(tm) # Framework for text mining.
  library(cluster)
  
  cname <- file.path(directory, "", "")
  docs <- Corpus(DirSource(cname))

  filtered<-tm_filter(docs[1:length(dir(cname))], FUN = function(x) meta(x)[["id"]] == fil)
  
  docs <- tm_map(docs, content_transformer(tolower))
  docs <- tm_map(docs, removeNumbers)
  docs <- tm_map(docs, removePunctuation)
  docs <- tm_map(docs, removeWords, stopwords("english"))
  docs <- tm_map(docs, stripWhitespace)
  docs <- tm_map(docs, stemDocument)
  
  
  dtm <- DocumentTermMatrix(docs)
  
  m <- as.matrix(dtm)
  
  library(proxy)
  
  ### this is going to take 4-ever (O(n^2))
  d <- dist(m, method="cosine")
  hc <- hclust(d, method="average")
  
}
