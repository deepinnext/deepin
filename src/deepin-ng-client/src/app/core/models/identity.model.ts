export interface TokenResponse {
    tokenType: string;
    accessToken: string;
    expireIn: number;
    refreshToken: string;
}

export interface LoginRequest {
    username: string;
    password: string;
    twoFactorCode?: string;
    twoFactorRecoveryCode?: string;
}

export interface RegisterRequest {
    email: string;
    password: string;
    confirmPassword: string;
}

export interface ResetPasswordRequest {
    email: string;
    newPassword: string;
    resetCode: string;
}
