// Source: https://medium.com/@velenra/using-angular-material-spinner-with-cdk-overlay-8ab92bfbafee
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { Overlay, OverlayRef } from '@angular/cdk/overlay';
import { scan, map, distinctUntilChanged } from 'rxjs/operators';
import { ComponentPortal } from '@angular/cdk/portal';
import { MatSpinner } from '@angular/material/progress-spinner';

@Injectable({
    providedIn: 'root'
})
export class SpinnerService {
    private spinnerRef: OverlayRef | undefined;

    constructor(private overlay: Overlay) {}

    show() {
        if (!this.spinnerRef) {
            const positionStrategy = this.overlay.position()
                .global()
                .centerHorizontally()
                .centerVertically();

            this.spinnerRef = this.overlay.create({
                hasBackdrop: true,
                positionStrategy
            });

            this.spinnerRef.attach(new ComponentPortal(MatSpinner));
        }
    }

    hide() {
        if (this.spinnerRef) {
            this.spinnerRef.detach();
            this.spinnerRef = undefined;
        }
    }
}
