import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AngularFireModule } from '@angular/fire/compat'
import { AngularFireStorage, AngularFireStorageModule } from '@angular/fire/compat/storage'

import { AngularFirestoreModule } from '@angular/fire/compat/firestore'
import { AngularFireAuthModule } from '@angular/fire/compat/auth'

import { provideFirebaseApp, initializeApp } from '@angular/fire/app';
import { getFirestore, provideFirestore } from '@angular/fire/firestore'
import { getStorage, provideStorage } from '@angular/fire/storage'

import { getAuth, provideAuth } from '@angular/fire/auth'
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { environment } from '@src/environments/environment';
import { IndicatorsModule } from './shared/indicators';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations'
import { PopupsModule } from './shared/popups';
import { NotificationModule } from './services';
import {MatSidenavModule} from '@angular/material/sidenav'
import {MatToolbarModule} from '@angular/material/toolbar'
import {MatIconModule} from '@angular/material/icon'
import {MatButtonModule} from '@angular/material/button';
import { HeaderComponent } from './components/header/header.component'
import {FlexLayoutModule} from '@angular/flex-layout';
import { MenuListComponent } from './components/menu-list/menu-list.component';
import {MatListModule} from '@angular/material/list';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { effects, reducers } from './store';
import { HttpClientModule } from '@angular/common/http';

const StoreDevTools = !environment.production ? StoreDevtoolsModule.instrument({maxAge:50} ) : []


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    MenuListComponent,


  ],
  imports: [
    BrowserModule,
    AppRoutingModule,

    provideFirebaseApp(() => initializeApp(environment.firebase.config)),
    provideFirestore(() => getFirestore()),
    provideStorage(() => getStorage()),
    provideAuth(() => getAuth()),

    AngularFireModule.initializeApp(environment.firebase.config),
    AngularFireStorageModule,
    AngularFirestoreModule,
    AngularFireAuthModule,

    IndicatorsModule,
    BrowserAnimationsModule,
    PopupsModule,

    NotificationModule.forRoot(),
    MatSidenavModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    FlexLayoutModule,
    MatListModule,
    StoreDevTools,
    StoreModule.forRoot(reducers,{
      runtimeChecks: {

        strictActionImmutability: true,
        strictStateImmutability: true
      }

    }),
    EffectsModule.forRoot(effects),
    HttpClientModule

  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
