docker network create health --label=health
docker-compose -f docker-compose.infrastructure.yml up -d
exit $LASTEXITCODE