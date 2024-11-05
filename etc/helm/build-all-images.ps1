./build-image.ps1 -ProjectPath "../../services/administration/Hola.Health.AdministrationService/Hola.Health.AdministrationService.csproj" -ImageName health/administration
./build-image.ps1 -ProjectPath "../../services/identity/Hola.Health.IdentityService/Hola.Health.IdentityService.csproj" -ImageName health/identity
./build-image.ps1 -ProjectPath "../../services/saas/Hola.Health.SaasService/Hola.Health.SaasService.csproj" -ImageName health/saas
./build-image.ps1 -ProjectPath "../../services/audit-logging/Hola.Health.AuditLoggingService/Hola.Health.AuditLoggingService.csproj" -ImageName health/auditlogging
./build-image.ps1 -ProjectPath "../../services/gdpr/Hola.Health.GdprService/Hola.Health.GdprService.csproj" -ImageName health/gdpr
./build-image.ps1 -ProjectPath "../../services/file-management/Hola.Health.FileManagementService/Hola.Health.FileManagementService.csproj" -ImageName health/filemanagement
./build-image.ps1 -ProjectPath "../../services/chat/Hola.Health.ChatService/Hola.Health.ChatService.csproj" -ImageName health/chat
./build-image.ps1 -ProjectPath "../../gateways/web/Hola.Health.WebGateway/Hola.Health.WebGateway.csproj" -ImageName health/webgateway
./build-image.ps1 -ProjectPath "../../apps/auth-server/Hola.Health.AuthServer/Hola.Health.AuthServer.csproj" -ImageName health/authserver
./build-image.ps1 -ProjectPath "../../apps/angular" -ImageName health/angular -ProjectType "angular"
./build-image.ps1 -ProjectPath "../../public/web/Hola.Health.Web.Public/Hola.Health.Web.Public.csproj" -ImageName health/webpublic
./build-image.ps1 -ProjectPath "../../gateways/public/Hola.Health.PublicGateway/Hola.Health.PublicGateway.csproj" -ImageName health/publicgateway
