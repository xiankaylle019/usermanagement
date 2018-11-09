export class AuthVM {
    username: string;
    password: string;

    deserialize(input: any): this {
        Object.assign(this, input);
        return this;
    }
}
