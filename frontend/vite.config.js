import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig({
	plugins: [react()],
	server: {
		host: '0.0.0.0', // Настройка для прослушивания на всех IP-адресах
		port: 5173, // Порт, на котором сервер будет работать
	},
})
