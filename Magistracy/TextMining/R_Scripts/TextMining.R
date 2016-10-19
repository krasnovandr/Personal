library(tm) # Framework for text mining.
library(rJava)
library(qdap) # Quantitative discourse analysis of transcripts.
library(qdapDictionaries)
library(dplyr) # Data wrangling, pipe operator %>%().
library(RColorBrewer) # Generate palette of colours for plots.
library(ggplot2) # Plot word frequencies.
library(scales) # Include commas in numbers
library(SnowballC)
library(magrittr)
library(Rgraphviz)
library(cluster)
library(fpc)
library(mclust)
library(apcluster)
cname <- file.path("E:/Users/Andrei/Desktop/R_Test", "", "")
length(dir(cname))
dir(cname)
docs <- Corpus(DirSource(cname))

docs
class(docs)
class(docs[[1]])
summary(docs)
inspect(docs[1])
docs <- tm_map(docs, content_transformer(tolower))
docs <- tm_map(docs, removeNumbers)
docs <- tm_map(docs, removePunctuation)
docs <- tm_map(docs, removeWords, stopwords("english"))
docs <- tm_map(docs, stripWhitespace)
docs <- tm_map(docs, stemDocument)
viewDocs <- function(d, n) {d %>% extract2(n) %>% as.character() %>% writeLines()}
viewDocs(docs, 1)
dtm <- DocumentTermMatrix(docs)
dtm

dtm_tfxidf <- weightTfIdf(dtm)
dtm_tfxidf
abc <- as.matrix(dtm)
tdm <- TermDocumentMatrix(docs)

freq <- colSums(as.matrix(dtm))
length(freq)
ord <- order(freq)
freq[head(ord)]
freq[tail(ord)]
m <- as.matrix(dtm)
dim(dtm)

# freq <- sort(colSums(as.matrix(dtm)), decreasing=TRUE)
# head(freq, 14)
# wf <- data.frame(word=names(freq), freq=freq)
# head(wf)
# library(ggplot2)
# subset(wf, freq>2) %>%
#   ggplot(aes(word, freq)) +
#   geom_bar(stat="identity") +
#   theme(axis.text.x=element_text(angle=45, hjust=1))
# 
library(wordcloud)
set.seed(123)
wordcloud(names(freq), freq, max.words = 100)
freq
set.seed(142)
res<-wordcloud(names(freq), freq, max.words=100, scale=c(5, .1), colors=brewer.pal(6, "Dark2"))
res

# dev.copy(jpeg,filename="plot.jpg");
# dev.off ();


# 
 # dtm_tfxidf <- weightTfIdf(dtm)
 # dtm_tfxidf
 # m <- as.matrix(dtm_tfxidf)
### cluster into 10 clusters
 ### don't forget to normalize the vectors so Euclidean makes sense
norm_eucl <- function(m) m/apply(m, MARGIN=1, FUN=function(x) sum(x^2)^.5)
 m_norm <- norm_eucl(m)

d <- data.frame( m_norm )
asw <- numeric(20)
documentCount <-nrow(d) - 1

d.apclus <- apcluster(negDistMat(r=2), m)
cat("affinity propogation optimal number of clusters:", n <- length(d.apclus@clusters), "\n")

for (k in 2:documentCount)
  asw[[k]] <- pam(d, k) $ silinfo $ avg.width
k.best <- which.max(asw)


cat("silhouette-optimal number of clusters:", k.best, "\n")

plot(1:20, asw, type= "h", main = "pam() clustering assessment",
     xlab= "k  (# clusters)", ylab = "average silhouette width")
axis(1, k.best, paste("best",k.best,sep="\n"), col = "red", col.axis = "red")
silinfo

m_norm

cl <- kmeans(m_norm, k.best,nstart = 20)
cl[1]
cl[1]



table(cl$cluster)
plot(prcomp(m_norm)$x, col=cl$cl)

library(proxy)

### this is going to take 4-ever (O(n^2))
d <- dist(m, method="cosine")
hc <- hclust(d, method="average")

# groups<-cutree(hc, k=5)
# groups

hc[1]
plot(hc)
plot(hc, hang = -1,horiz= TRUE)
hc[2]
# cl <- cutree(hc, 3)
# cl[0]
# cl[1]
table(cl)  cl


