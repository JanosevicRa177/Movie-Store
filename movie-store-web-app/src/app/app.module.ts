import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CustomerModule } from './customer/customer.module';
import { MovieModule } from './movie/movie.module';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { SharedModule } from './shared/shared.module';
import { ToastrModule } from 'ngx-toastr';

import { MsalInterceptor, MsalModule } from "@azure/msal-angular";
import { InteractionType, PublicClientApplication } from "@azure/msal-browser";

@NgModule({
    declarations: [AppComponent],
    imports: [
        BrowserModule,
        AppRoutingModule,
        BrowserAnimationsModule,
        CustomerModule,
        MovieModule,
        HttpClientModule,
        SharedModule,
        ToastrModule.forRoot(),
        MsalModule.forRoot(
            new PublicClientApplication({
                auth: {
                    clientId: '4e1ff54b-bf34-4f45-83ce-e50fc32967cd',
                    authority: 'https://login.microsoftonline.com/common',
                    redirectUri: 'http://localhost:4200',
                },
                cache: {
                    cacheLocation: "localStorage"
                },
            }), null!, {
            // MSAL Interceptor Configurations
            interactionType: InteractionType.Redirect,
            protectedResourceMap: new Map([
                ["http://localhost:8085", ['api://dbf7f51e-d046-435b-88ee-c4f9ee872967/to-do-lists.read', 'api://dbf7f51e-d046-435b-88ee-c4f9ee872967/to-do-lists.write']],
            ])
        }
        ),
    ],
    providers: [
        {
            provide: HTTP_INTERCEPTORS,
            useClass: MsalInterceptor,
            multi: true
        },],
    bootstrap: [AppComponent],
})
export class AppModule { }
