# Magazine Website

Coming soon...

# Getting Started

## Download / Clone

Clone the repo using Git:

`git clone https://github.com/thangchung/magazine-website.git`

## Boot up the whole system

`docker-compose -f build/docker-compose.yml up -d`

`docker-compose logs`

## Run each service

`docker build -f build/Dockerfile.MagazineService -t thangchung/magazine_service .`

`docker run -d -p 5000:5000 -t thangchung/magazine_service`

# Versioning

For transparency into our release cycle and in striving to maintain backward compatibility, MazWebApp is maintained under the [the Semantic Versioning guidelines](http://semver.org/). Sometimes we screw up, but we'll adhere to those rules whenever possible.

# Want to contribute?

If you found a bug, have any questions or want to contribute. Follow our guidelines, and help improve the MazWebApp. For more information visit our wiki.

# License

© ThangChung, 2016. Licensed under an MIT license.
