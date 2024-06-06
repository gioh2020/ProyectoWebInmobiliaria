export interface User {
    id: number;
    username: string;
    nombre: string;
    apellido: string;
    telefono: string;
    email: string;
    token: string
}

export interface Token {
    refresh: string;
    access: string;
}