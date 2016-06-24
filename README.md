# Magazine Website (stand for Magazine Website + Application)

Coming soon...

# Getting Started

## Download / Clone

Clone the repo using Git:

`git clone https://github.com/thangchung/magazine-website.git`

## Database

`docker pull postgres:9.6`

`docker run --name magazine_db -p 5432:5432 -e POSTGRES_DB=magazine_db -e POSTGRES_USER=magazine_dba -e POSTGRES_PASSWORD=Passw0rd -d postgres:9.6`

## Build on Development Environment

`dotnet restore`

`dotnet build`

`dotnet run`

## Build on Production Environment (docker)

`docker build -f Dockerfile.MagazineService -t thangchung/magazine_service .`

`docker run -d -p 5000:5000 -t thangchung/magazine_service`

 ## Build on Production Environment (docker-compose)

`docker-compose up -d` or `docker-compose up --build` for re-build a package. 

# Versioning

For transparency into our release cycle and in striving to maintain backward compatibility, MazWebApp is maintained under the [the Semantic Versioning guidelines](http://semver.org/). Sometimes we screw up, but we'll adhere to those rules whenever possible.

# Want to contribute?

If you found a bug, have any questions or want to contribute. Follow our guidelines, and help improve the MazWebApp. For more information visit our wiki.

# License

© ThangChung, 2016. Licensed under an MIT license.