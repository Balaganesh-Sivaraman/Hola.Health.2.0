import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

const oAuthConfig = {
  issuer: 'http://localhost:44340/',
  redirectUri: baseUrl,
  clientId: 'Angular',
  responseType: 'code',
  scope: 'offline_access openid profile email phone AuthServer IdentityService AdministrationService AuditLoggingService GdprService ChatService SaasService FileManagementService',
  requireHttps: false,
  impersonation: {
    tenantImpersonation: true,
    userImpersonation: true,
  }
};

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'Health',
  },
  oAuthConfig,
  apis: {
    default: {
      url: 'http://localhost:44322',
      rootNamespace: 'Hola.Health',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
  },
} as Environment;
