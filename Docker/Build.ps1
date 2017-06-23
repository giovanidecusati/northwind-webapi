# Build a image to build app
docker build --tag "giovanidecusati/northwind-webapi:dev" -f .\Docker\Dockerfile.build .

# Create a container
docker create --name nwapi-build giovanidecusati/northwind-webapi:dev

# Copy the publish folder from the builded container
docker cp nwapi-build:/out ./publish

# Build a runtime (compiled) image
docker build --tag "giovanidecusati/northwind-webapi:0.0.1" -f .\Docker\Dockerfile.runtime .

# Run Web Api
docker run --rm -p 5000:80 giovanidecusati/northwind-webapi:0.0.1