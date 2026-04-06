export interface TokenRequest {
    RefreshToken: string | null;
    ClientId: string;
    Scope: string;
    Username: string | null;
    Password: string | null;
    GrantType: string;
  }
