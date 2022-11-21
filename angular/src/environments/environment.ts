import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'TestLoginYogapoint',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44335/',
    redirectUri: baseUrl,
    clientId: 'TestLoginYogapoint_App',
    responseType: 'code',
    scope: 'offline_access TestLoginYogapoint',
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:44335',
      rootNamespace: 'TestLoginYogapoint',
    },
  },
} as Environment;
