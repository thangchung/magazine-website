FROM microsoft/dotnet

MAINTAINER Thang Chung "thangchung@ymail.com"

# Set environment variables
# ENV ASPNETCORE_URLS="http://*:8888"
ENV ASPNETCORE_ENVIRONMENT="Development"

# Copy files to app directory
COPY /publish/auth_service/ /root/src/Services/Cik.Services.Auth.AuthService
COPY /publish/Config/ /root/src/Config

# Set working directory
WORKDIR /root/src/Services/Cik.Services.Auth.AuthService/

# Open up port
EXPOSE 8888

# Run the app
ENTRYPOINT ["dotnet", "/root/src/Services/Cik.Services.Auth.AuthService/Cik.Services.Auth.AuthService.dll"]
