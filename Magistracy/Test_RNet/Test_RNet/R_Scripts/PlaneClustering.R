function (dtm,folderName) {
  m <- as.matrix(dtm)
  
  ### don't forget to normalize the vectors so Euclidean makes sense
  norm_eucl <- function(m) m/apply(m, MARGIN=1, FUN=function(x) sum(x^2)^.5)
  m_norm <- norm_eucl(m)
  
  for (k in 2:documentCount)
    asw[[k]] <- pam(d, k) $ silinfo $ avg.width
  k.best <- which.max(asw)
  
  cl <- kmeans(m_norm, k.best,nstart = 20)
}
