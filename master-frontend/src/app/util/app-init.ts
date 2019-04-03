import { KeycloakService } from 'keycloak-angular';

export function initializer(keycloak: KeycloakService) {
    return (): Promise<any> => {
        return new Promise(async (resolve, reject) => {
            try {
                await keycloak.init({
                    config: {
                      url: 'http://hrzagwn111960:8080/auth',
                      realm: 'master',
                      clientId: 'AVLCarSystemClient',
                      credentials: {
                          secret: '0ed4f846-3455-4d63-a450-637614edfa4f'
                      }
                    },
                    initOptions: {
                      onLoad: 'check-sso',
                      checkLoginIframe: false
                    },
                    enableBearerInterceptor: true,
                    bearerExcludedUrls: [
                        '/dashboard'                    ],
                  });
                resolve();
            } catch (error) {
                reject(error);
            }
        });
    };
}
