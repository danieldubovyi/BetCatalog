import { fileURLToPath, URL } from 'node:url';
import { defineConfig, searchForWorkspaceRoot } from 'vite';
import plugin from '@vitejs/plugin-react';
import fs from 'fs';
import path from 'path';
import child_process from 'child_process';
import { env } from 'process';

const isDevelopment = env.NODE_ENV !== 'production'; // Check if the environment is development

let httpsConfig: { key: Buffer; cert: Buffer } | undefined = undefined; // Default to undefined when not using HTTPS

if (isDevelopment) {
    const baseFolder =
        env.APPDATA !== undefined && env.APPDATA !== ''
            ? `${env.APPDATA}/ASP.NET/https`
            : `${env.HOME}/.aspnet/https`;

    const certificateName = "DroneAssembly.WebUI";
    const certFilePath = path.join(baseFolder, `${certificateName}.pem`);
    const keyFilePath = path.join(baseFolder, `${certificateName}.key`);

    if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath)) {
        if (0 !== child_process.spawnSync('dotnet', [
            'dev-certs',
            'https',
            '--export-path',
            certFilePath,
            '--format',
            'Pem',
            '--no-password',
        ], { stdio: 'inherit' }).status) {
            throw new Error("Could not create certificate.");
        }
    }

    httpsConfig = {
        key: fs.readFileSync(keyFilePath),
        cert: fs.readFileSync(certFilePath),
    };
}

const target = env.ASPNETCORE_HTTPS_PORT ? `http://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
    env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:5001';

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [plugin()],
    resolve: {
        alias: {
            '@': fileURLToPath(new URL('./src', import.meta.url)),
        },
    },
    server: {
        fs: {
            allow: [
                searchForWorkspaceRoot(process.cwd()),
                './src/main.tsx',
            ],
        },
        proxy: {
            '^/weatherforecast': {
                target,
                secure: false,
            },
            '^/api/components': {
                target: 'http://localhost:5000/',
                secure: false,
            },
            '^/api/drones': {
                target: 'http://localhost:5000/',
                secure: false,
            },
        },
        port: 5173,
        https: httpsConfig, // Use the https configuration or undefined
    },
});