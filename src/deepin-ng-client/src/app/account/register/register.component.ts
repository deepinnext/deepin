import { NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButton, MatIconButton } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIcon } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { Router, RouterLink } from '@angular/router';
import { ACOCUNT_ROUTERS } from '../../core/constants/route.config';
import { AuthService } from '../../core/services/auth.service';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrl: './register.component.scss',
    imports: [
        NgIf,
        RouterLink,
        ReactiveFormsModule,
        MatCardModule,
        MatFormFieldModule,
        MatInputModule,
        MatButton,
        MatIconButton,
        MatIcon
    ]
})
export class RegisterComponent {
  form?: FormGroup;
  isLoading = false;
  showPassword = false;
  showComfirmPassword = false;

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {
    this.form = this.fb.group(
      {
        email: this.fb.control('', [Validators.required, Validators.email]),
        password: this.fb.control('', [Validators.required, Validators.minLength(8)]),
        confirmPassword: this.fb.control('', [Validators.required]),
      }, {
        validators: this.passwordMatchValidator
      } as AbstractControlOptions);
  }

  passwordMatchValidator(form: FormGroup) {
    return form.get('password')?.value === form.get('confirmPassword')?.value ? null : { passwordMismatch: true };
  }

  onSubmit() {
    if (this.isLoading || this.form?.invalid) return;
    this.isLoading = true;
    this.authService.register(this.form?.value)
      .subscribe({
        next: () => {
          this.router.navigate(['/', ACOCUNT_ROUTERS.root, ACOCUNT_ROUTERS.registerConfirmation], { queryParams: { email: this.form?.value.email } });
        },
        error: (err) => {
          console.error(err);
        },
        complete: () => {
          this.isLoading = false;
        }
      });
  }
}
