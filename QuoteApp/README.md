# Txt


./sync_server --urls=http://0.0.0.0:10000/

dotnet build -r linux-x64 /p:PublishReadyToRun=true

docker build -t documentconversionwebapi .
docker run -d -p 8080:80 documentconversionwebapi
 
    Info
        https://blog.rsuter.com/automatically-migrate-your-entity-framework-core-managed-database-on-asp-net-core-application-start/
        https://postgis.net/docs/manual-dev/postgis_usage.html
        https://www.npgsql.org/efcore/mapping/nts.html
        https://gis.stackexchange.com/questions/11567/spatial-clustering-with-postgis


    CREATE SERVICE
        nano /etc/systemd/system/wa_sync_server.service
        systemctl enable wa_sync_server.service
        systemctl start wa_sync_server


    METRICS
        https://www.app-metrics.io/samples/grafana/



    CLUSTERING POINTS
        https://gis.stackexchange.com/questions/11567/spatial-clustering-with-postgis


    LINQ EXTENSIONS
        PostgisExtensions
 
    INSTALL POSTGRESS 12
        https://computingforgeeks.com/how-to-install-postgresql-12-on-centos-7/


    INSTALL POSTGIST
        https://people.planetpostgresql.org/devrim/index.php?/archives/102-Installing-PostGIS-3.0-and-PostgreSQL-12-on-CentOS-8.html
 
    SCREEN 

        screen -S name
        screen -list        
        screen -r name
 
    JENKINS
        https://medium.com/fusionqa/how-to-run-jenkins-using-the-root-user-in-linux-centos-79d96749ca5a
        docker run --name jenkins-docker -p 8080:8080 -v /var/run/docker.sock:/var/run/docker.sock gustavoapolinario/jenkins-docker
 
    
    file="/etc/systemd/system/wa_unipos.service"
    if [ -f "$file" ]
    then
	    systemctl stop wa_unipos
    else
	    cp waunipos.service /etc/systemd/system/wa_unipos.service
        systemctl enable wa_unipos.service
    fi
    
    systemctl start wa_unipos
     
    DOCKER
        https://itnext.io/smaller-docker-images-for-asp-net-core-apps-bee4a8fd1277


