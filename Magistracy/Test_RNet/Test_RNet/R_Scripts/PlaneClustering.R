function (dtm,folderName) {
  # m <- as.matrix(dtm)
  # 
  # ### don't forget to normalize the vectors so Euclidean makes sense
  # norm_eucl <- function(m) m/apply(m, MARGIN=1, FUN=function(x) sum(x^2)^.5)
  # m_norm <- norm_eucl(m)
  # 
  # for (k in 2:documentCount)
  #   asw[[k]] <- pam(d, k) $ silinfo $ avg.width
  # k.best <- which.max(asw)
  # 
  # cl <- kmeans(m_norm, k.best,nstart = 20)
  
  
  
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
  
  
  
  directoryPathNormalized <-normalizePath(folderName)
  fullPath <- file.path(directoryPathNormalized,"planeClustering.png")
  png(fullPath)
  plot(prcomp(m_norm)$x, col=cl$cl)  
  dev.off()
  
  typeof(cl$cluster)
  typeof(cl$cluster[1])
  
  
  
  frame <- as.data.frame(cl[1])
  frame
}
