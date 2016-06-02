# Magazine Website (stand for Magazine Website + Application)

Coming soon...

# Getting Started

## Download / Clone

Clone the repo using Git:

`git clone https://github.com/thangchung/magazine-website.git`

## Build

`dotnet restore`

`dotnet build`

`dotnet run`

## Docker

`docker build -f Dockerfile.CategoryService -t tc/category_service .`

`docker run -d -p 5000:5000 -t tc/category_service`

At the moment, we have to exec to the container to install sqlite version due to some bugs in microsoft/dotnet image

`docker exec -it <container id> /bin/bash`

`apt-get update`

`apt-get install sqlite3 libsqlite3-dev`

 then re-start container again  

# Versioning

For transparency into our release cycle and in striving to maintain backward compatibility, MazWebApp is maintained under the [the Semantic Versioning guidelines](http://semver.org/). Sometimes we screw up, but we'll adhere to those rules whenever possible.

# Want to contribute?

If you found a bug, have any questions or want to contribute. Follow our guidelines, and help improve the MazWebApp. For more information visit our wiki.

# License

© ThangChung, 2016. Licensed under an MIT license.