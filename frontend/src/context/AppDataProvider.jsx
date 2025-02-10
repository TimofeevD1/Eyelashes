import React, { createContext, useState, useEffect } from 'react'
import ApiService from '../services/ApiService'

export const AppDataContext = createContext(null)

const AppDataProvider = ({ children }) => {
	const [aboutMeData, setAboutMeData] = useState(null)
	// const [servicesData, setServicesData] = useState(null)
	// const [locationData, setLocationData] = useState(null)
	const [loading, setLoading] = useState(true)

	useEffect(() => {
		const fetchData = async () => {
			try {
				const aboutMe = await ApiService.getAboutMe()
				// const services = await ApiService.getServices()
				// const location = await ApiService.getLocation()

				setAboutMeData(aboutMe)
				// setServicesData(services)
				// setLocationData(location)
			} catch (error) {
				console.error('Ошибка загрузки данных:', error)
			} finally {
				setLoading(false)
			}
		}

		fetchData()
	}, [])

	return (
		<AppDataContext.Provider
			// value={{ aboutMeData, servicesData, locationData, loading }}
			value={{ aboutMeData, loading }}
		>
			{children}
		</AppDataContext.Provider>
	)
}

export default AppDataProvider
