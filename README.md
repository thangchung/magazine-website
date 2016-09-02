# Magazine Website

Coming soon...

# Getting Started

## Download / Clone

Clone the repo using Git:

`git clone https://github.com/thangchung/magazine-website.git`

## Boot up the whole system

> The development environment on Windows (not on Linux or MacOS)

> Make sure you installed .NET Core, Powershell and Docker Toolbox for Windows to make it work 

> Make sure you `cd` into the magazine-website root folder to run all commands below

`docker-machine create --driver virtualbox default`

`FOR /f "tokens=*" %i IN ('docker-machine env default') DO %i`

`powershell -f deploy/build-all.ps1`

`docker-compose -f deploy/docker-compose.yml up -d`

`docker-compose logs`

## Run each service

`docker build -f deploy/Dockerfile.MagazineService -t thangchung/magazine_service .`

`docker run -d -p 5000:5000 -t thangchung/magazine_service`

# Versioning

For transparency into our release cycle and in striving to maintain backward compatibility, MazWebApp is maintained under the [the Semantic Versioning guidelines](http://semver.org/). Sometimes we screw up, but we'll adhere to those rules whenever possible.

# Want to contribute?

If you found a bug, have any questions or want to contribute. Follow our guidelines, and help improve the MazWebApp. For more information visit our wiki.

# License

© ThangChung, 2016. Licensed under an MIT license.
