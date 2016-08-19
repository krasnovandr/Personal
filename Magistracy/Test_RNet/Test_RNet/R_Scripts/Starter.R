function(folderName,directoryPath) {
  
  library(tm) # Framework for text mining.
  library(cluster)
  # library(hclust)
  
   cname <- file.path("E:/Users/Andrei/Desktop/R_Test", "", "")

  # cname <- file.path(folderName, "", "")
  fil<-c("1.txt", "2.txt", "3.txt") 
  docs <- Corpus(DirSource(cname))
  meta(docs[[1]])
  filtered<-tm_filter(docs[1:8], FUN = function(x) meta(x)[["id"]] == fil)
  filtered
  docs
  docs <- tm_map(docs, content_transformer(tolower))
  docs <- tm_map(docs, removeNumbers)
  docs <- tm_map(docs, removePunctuation)
  docs <- tm_map(docs, removeWords, stopwords("english"))
  docs <- tm_map(docs, stripWhitespace)
  docs <- tm_map(docs, stemDocument)

  
  dtm <- DocumentTermMatrix(docs)

  # scriptPath <- file.path(directoryPath,"WordCloud.R")
  # cloud <- dget(scriptPath)
  # cloud(dtm,folderName)
  # 
  # 
  # scriptPath <- file.path(directoryPath,"PlaneClustering.R")
  # clustering <- dget(scriptPath)
  # cloud(dtm,folderName)
  

  m <- as.matrix(dtm)
  
  ### don't forget to normalize the vectors so Euclidean makes sense
  norm_eucl <- function(m) m/apply(m, MARGIN=1, FUN=function(x) sum(x^2)^.5)
  m_norm <- norm_eucl(m)
  d <- data.frame( m_norm )
  asw <- numeric(20)
  documentCount <-nrow(d) - 1
  for (k in 2:documentCount)
    asw[[k]] <- pam(d, k) $ silinfo $ avg.width
  k.best <- which.max(asw)

  
  cat("silhouette-optimal number of clusters:", k.best, "\n")
 
  plot(1:20, asw, type= "h", main = "pam() clustering assessment",
       xlab= "k  (# clusters)", ylab = "average silhouette width")
  axis(1, k.best, paste("best",k.best,sep="\n"), col = "red", col.axis = "red")

  
  cl <- kmeans(m_norm, k.best,nstart = 20)

  table(cl$cluster)
  plot(prcomp(m_norm)$x, col=cl$cl)

  typeof(cl$cluster)
  typeof(cl$cluster[1])
  


  frame <- as.data.frame(cl[1])
  frame
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
