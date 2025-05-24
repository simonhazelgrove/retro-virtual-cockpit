# Hacks

Remove these as & when able.

## NPM Patches

These instructions have been followed to generate the following patches: https://dev.to/zhnedyalkow/the-easiest-way-to-patch-your-npm-package-4ece

### gh-pages

Using gh-pages v6.3.0 npm package, I am getting this error when running 'npm run deploy':

```
Error: spawn ENAMETOOLONG
```

This file shows a patch that can be generated to get around this bug: https://github.com/tschaub/gh-pages/issues/585

What to remove:

- patches folder
- postinstall script in package.json
- patch-package devDependency