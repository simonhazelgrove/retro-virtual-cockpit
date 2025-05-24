// Used to declare audio file types for TypeScript to avoid problems with importing audio files in React components.
declare module "*.wav" {
  const src: string;
  export default src;
}