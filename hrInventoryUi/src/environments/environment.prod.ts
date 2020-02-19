import { $ENV } from 'typing';

export const environment = {
  production: true,
  //API_URL:"http://localhost:5000/api/"
  API_URL:$ENV.API_URL
};

