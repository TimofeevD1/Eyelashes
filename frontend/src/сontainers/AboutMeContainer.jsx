import React, { useState, useEffect } from 'react'
import axios from 'axios'
import AboutMeSection from '../components/aboutMeSection/AboutMeSection'

const AboutMeContainer = ({ scrollToOrderNow }) => {
	const [aboutMeData, setAboutMeData] = useState(null)
	const [loading, setLoading] = useState(true)
	const [error, setError] = useState(null)

	useEffect(() => {
		const fetchAboutMeData = async () => {
			try {
				const response = await axios.get(
					'http://localhost:5284/api/AboutMe/get'
				)
				setAboutMeData(response.data)
			} catch (err) {
				setError(err.message)
			} finally {
				setLoading(false)
			}
		}

		fetchAboutMeData()
	}, [])

	if (loading) return <p>Загрузка...</p>
	if (error) return <p>Ошибка: {error}</p>

	return (
		<div className='p-8'>
			{aboutMeData && (
				<AboutMeSection
					scrollToOrderNow={scrollToOrderNow}
					fullName={aboutMeData.fullName}
					job={aboutMeData.job}
					description={aboutMeData.description}
					mainImagePath={aboutMeData.mainImagePath}
					additionalPhotosPath={aboutMeData.additionalPhotosPath}
				/>
			)}
		</div>
	)
}

export default AboutMeContainer
