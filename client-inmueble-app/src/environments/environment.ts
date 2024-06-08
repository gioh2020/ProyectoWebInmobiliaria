// This file can be replaced during build by using the `fileReplacements` array.
// `ng build` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  name: 'dev',
  firebase: {
    config: {
      apiKey: "AIzaSyBV9Wl4bIQXb3i-T6wzIPwT-GfyJsjglf0",
      authDomain: "edificaion-app.firebaseapp.com",
      projectId: "edificaion-app",
      storageBucket: "edificaion-app.appspot.com",
      messagingSenderId: "1028797408734",
      appId: "1:1028797408734:web:457604d67c23c4666ea94f"
    }
  },
   url: 'http://localhost:5000/'
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.
