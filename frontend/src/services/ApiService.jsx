import axios from 'axios'

class ApiService {
	constructor(baseURL) {
		this.api = axios.create({
			baseURL,
		})
	}

	async getAboutMe() {
		try {
			const response = await this.api.get('/api/AboutMe/get')
			return response.data
		} catch (error) {
			console.error('Ошибка при получении данных "Обо мне":', error)
			return null
		}
	}
}

export default new ApiService('http://localhost:5284')
