import React, { useContext } from 'react'
import { AppDataContext } from '../context/AppDataProvider'
import AboutMeSection from '../components/aboutMeSection/AboutMeSection'

const AboutMeContainer = ({ scrollToOrderNow }) => {
	const { aboutMeData, loading } = useContext(AppDataContext)

	if (loading) {
		return (
			<div className='flex justify-center items-center h-40'>
				<div className='w-12 h-12 border-4 border-purple-200 border-t-transparent rounded-full animate-spin'></div>
			</div>
		)
	}

	if (!aboutMeData) {
		return (
			<div className='text-center text-red-500 p-6'>
				<h2 className='text-xl font-bold'>Ошибка загрузки данных</h2>
				<p>Попробуйте обновить страницу.</p>
			</div>
		)
	}

	return (
		<div className='p-5'>
			<AboutMeSection {...aboutMeData} scrollToOrderNow={scrollToOrderNow} />
		</div>
	)
}

export default AboutMeContainer
