export function convertStringToCamelCase(superName: string): string {
    return superName.charAt(0).toLowerCase() + superName.slice(1);
}