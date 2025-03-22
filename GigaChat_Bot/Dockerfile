FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build

COPY . /source

WORKDIR /source

ARG TARGETARCH

RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish -a ${TARGETARCH/amd64/x64} --use-current-runtime --self-contained false -o //Mega-server/data/Projects/GigaChat/app

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS final
WORKDIR //Mega-server/data/Projects/GigaChat/app

COPY --from=build //Mega-server/data/Projects/GigaChat/app .

ARG UID=10001
RUN adduser \
    --disabled-password \
    --gecos "" \
    --home "/nonexistent" \
    --shell "/sbin/nologin" \
    --no-create-home \
    --uid "${UID}" \
    appuser
USER appuser

ENTRYPOINT ["dotnet", "GigaChat_Bot.dll"]
