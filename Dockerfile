FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 4648
EXPOSE 44303

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["MightyHomeAutomation.csproj", ""]
RUN dotnet restore "/MightyHomeAutomation.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "MightyHomeAutomation.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MightyHomeAutomation.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MightyHomeAutomation.dll"]