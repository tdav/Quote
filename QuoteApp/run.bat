dotnet publish --runtime alpine-x64 --self-contained true /p:PublishTrimmed=true /p:PublishSingleFile=true -c Release -o ./publish

dotnet ef migrations script --idempotent --output "./publish/EFSQLScripts/Sync.MyDbContext.sql" --context Sync.MyDbContext 

docker build -t web1 -f Dockerfile .

docker run -p 8080:80 web1


docker run --name tdav/syncserver -p 20005:20000 -e "DefaultConnection=Host=localhost;Database=apteka_surhon;Username=postgres;Password=grand_online_orders;Pooling=true;"  -d sync_surhon
