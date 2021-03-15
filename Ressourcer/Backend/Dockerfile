FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim
ADD ./ /app
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENTRYPOINT [ "dotnet", "F20ITONK.ASPNETCore31.Microservice.Backend.dll"]