# Battleships
Simple but extendable Battleship game

# Why and What?
1) DDD
   1) Separation of Responsibility
   2) Flexibility and Scalability
   3) Long-term maintanance
2) DI
   1) Decoupling and Separation of Concerns
   2) Configurability and Flexibility
   3) Testability
3) IOptions
   1) Easy to implement
   2) Code and Configuration separation
4) CPM
   1) Because dependency management is easier in multi-project solution
5) ... and many more not listed here :)

# How to run
1) Ensure you have `docker` installed
2) Build docker image
```cmd
docker build -t battleships-app:1.0 .
```
3) Run docker image
```
docker run -it --rm --name Battleships battleships-app:1.0
```
