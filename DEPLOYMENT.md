# Container Damage Assessment - Integrated Frontend & API

This project now combines both the frontend and API into a single ASP.NET Core web application for simplified deployment.

## ?? Project Structure

```
src/api/
??? Controllers/          # API Controllers
??? wwwroot/             # Frontend static files
?   ??? index.html       # Main web application
??? Program.cs           # Application entry point
??? Dockerfile           # Docker configuration
```

## ?? Running the Application

### Development (Local)

1. **Run the API project:**
   ```bash
   cd src/api
   dotnet run
   ```

2. **Access the application:**
   - Frontend: https://localhost:7162/ (or http://localhost:5000)
   - API: https://localhost:7162/api/Container
   - Swagger: https://localhost:7162/swagger

### Production (Docker)

1. **Build Docker image:**
   ```bash
   docker build -t container-damage-ai -f src/api/Dockerfile .
   ```

2. **Run container:**
   ```bash
   docker run -p 8080:8080 container-damage-ai
   ```

3. **Access the application:**
   - http://localhost:8080

## ? Key Features

- **Single Deployment**: One web app serves both UI and API
- **Static File Serving**: Frontend served from wwwroot folder
- **Relative URLs**: API calls use relative paths (works in all environments)
- **Docker Ready**: Dockerfile includes both frontend and backend
- **Azure App Service Ready**: Deploy as single web app

## ?? Configuration

### API Endpoints

The frontend makes calls to these API endpoints:
- `POST /api/Container/upload` - Upload and analyze container image
- `GET /api/Container/download/{imageId}` - Download processed image

### Azure Storage Configuration

Update `appsettings.json` or `appsettings.local.json`:

```json
{
  "AzureStorage": {
    "ConnectionString": "YOUR_AZURE_STORAGE_CONNECTION_STRING",
    "ContainerName": "container-images"
  }
}
```

## ?? Deployment to Azure

### Option 1: Azure App Service (Recommended)

1. **Using Azure CLI:**
   ```bash
   az webapp up --name your-app-name --resource-group your-rg
   ```

2. **Using Visual Studio:**
   - Right-click on `api` project ? Publish
   - Select Azure App Service
   - Configure and deploy

### Option 2: Azure Container Instances

1. **Push to Azure Container Registry:**
   ```bash
   az acr build --registry yourregistry --image container-damage-ai:latest .
   ```

2. **Deploy to ACI:**
   ```bash
   az container create --resource-group your-rg \
     --name container-damage-ai \
     --image yourregistry.azurecr.io/container-damage-ai:latest \
     --dns-name-label container-damage-ai \
     --ports 8080
   ```

## ?? Benefits of Integrated Deployment

1. **Simplified Infrastructure**: One web app instead of separate frontend and backend
2. **Reduced Costs**: Single Azure App Service instead of two services
3. **Easier CORS Management**: No cross-origin issues
4. **Better Performance**: Reduced network latency
5. **Simplified CI/CD**: Single pipeline for both frontend and backend

## ?? Development Workflow

### Adding Frontend Changes

1. Edit files in `src/api/wwwroot/`
2. Refresh browser (no rebuild needed for HTML/CSS/JS changes)

### Adding Backend Changes

1. Edit C# files in respective projects
2. Rebuild and restart the application

## ?? Notes

- The frontend uses relative URLs (`/api/...`) so it works in both development and production
- Static files are served from the `wwwroot` folder
- Swagger UI is available at `/swagger` for API testing
- The application serves `index.html` as the default page
