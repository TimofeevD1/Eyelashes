import React, { useEffect, useState } from 'react'
import { YMaps, Map, Placemark } from '@r3flector/react-yandex-maps'
import {
	FaFacebookF,
	FaInstagram,
	FaTwitter,
	FaLinkedinIn,
} from 'react-icons/fa'
import { motion } from 'framer-motion'
import { useInView } from 'react-intersection-observer'

const LocationInfo = ({ address, phoneNumber, coordinates }) => {
	const coordinatesLoc = [coordinates.x, coordinates.y]

	const socialLinks = [
		{
			icon: FaFacebookF,
			href: 'https://facebook.com',
			color: 'hover:text-blue-400',
		},
		{
			icon: FaInstagram,
			href: 'https://instagram.com',
			color: 'hover:text-pink-400',
		},
		{
			icon: FaTwitter,
			href: 'https://twitter.com',
			color: 'hover:text-blue-300',
		},
		{
			icon: FaLinkedinIn,
			href: 'https://linkedin.com',
			color: 'hover:text-blue-800',
		},
	]

	const { ref: mapRef, inView: mapInView } = useInView({
		triggerOnce: true,
		threshold: 0.5,
	})
	const { ref: infoRef, inView: infoInView } = useInView({
		triggerOnce: true,
		threshold: 0.5,
	})

	const [windowSize, setWindowSize] = useState({
		width: window.innerWidth,
		height: window.innerHeight,
	})
	const [mapKey, setMapKey] = useState(Date.now()) // Ключ для рендеринга компонента

	// Слежение за изменениями окна
	useEffect(() => {
		const handleResize = () => {
			setWindowSize({ width: window.innerWidth, height: window.innerHeight })

			// Меняем ключ для перерендеривания компонента
			setMapKey(Date.now())
		}

		// Добавляем слушатель на изменение размера окна
		window.addEventListener('resize', handleResize)

		// Очистка при размонтировании компонента
		return () => {
			window.removeEventListener('resize', handleResize)
		}
	}, [])

	return (
		<>
			{/* Обновляем key при изменении размера окна */}
			<div key={mapKey} className='flex flex-col md:flex-row w-full h-full'>
				{/* Карта */}
				<motion.div
					ref={mapRef}
					className='w-full md:w-2/3 h-[400px] md:h-[500px]'
					initial={{ opacity: 0, x: -100 }}
					animate={{ opacity: mapInView ? 1 : 0, x: mapInView ? 0 : -100 }}
					transition={{ duration: 1 }}
				>
					<YMaps query={{ apikey: 'c68fac64-56d9-48be-9212-0e1679833315' }}>
						<Map
							defaultState={{
								center: coordinatesLoc,
								zoom: 15,
								controls: ['zoomControl', 'fullscreenControl'],
							}}
							modules={['control.ZoomControl', 'control.FullscreenControl']}
							width='100%'
							height='100%'
						>
							<Placemark
								geometry={coordinatesLoc}
								properties={{
									hintContent: 'Наш офис',
									balloonContent: `<strong>${'Наш офис'}</strong><br/>${address}`,
								}}
								options={{
									preset: 'islands#redDotIcon',
								}}
							/>
						</Map>
					</YMaps>
				</motion.div>

				{/* Информация */}
				<motion.div
					ref={infoRef}
					className='w-full md:w-1/3 p-6 flex flex-col justify-start bg-gradient-to-r from-pink-200 via-pink-100 to-pink-50 rounded-l-3xl shadow-xl'
					initial={{ opacity: 0, x: 100 }}
					animate={{ opacity: infoInView ? 1 : 0, x: infoInView ? 0 : 100 }}
					transition={{ duration: 1, type: 'spring', stiffness: 100 }}
				>
					<h2 className='text-3xl font-bold text-gray-800 drop-shadow-md'>
						Наш офис
					</h2>
					<p className='mt-4 text-gray-700 text-lg'>Адрес: {address}</p>
					<p className='mt-2 text-gray-700 text-lg'>Телефон: {phoneNumber}</p>

					{/* Социальные сети */}
					<div className='mt-6 flex space-x-6'>
						{socialLinks.map(({ icon: Icon, href, color }, index) => (
							<a
								key={index}
								href={href}
								target='_blank'
								rel='noopener noreferrer'
								className={`text-gray-800 ${color} transform transition-all duration-300 hover:scale-110`}
								aria-label={`Перейти на ${href}`}
							>
								<Icon size={28} />
							</a>
						))}
					</div>
				</motion.div>
			</div>

			{/* Футер */}
			<footer className='w-full bg-pink-300 text-gray-800 py-6 text-center'>
				<p className='text-xl font-medium'>© 2025, Все права защищены</p>
			</footer>
		</>
	)
}

export default LocationInfo
