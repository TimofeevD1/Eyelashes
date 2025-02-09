import React, { useRef, useState } from 'react'
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import contactData from './helpers/contactData '
import promoData from './helpers/promoData'
import ContactInfo from './components/сontactInfo/ContactInfo '
import LashExtensionsPromo from './components/lashExtensionsPromo/LashExtensionsPromo'
import OrderNow from './components/orderNow/OrderNow'
import ServicesSection from './components/servicesSection/ServicesSection'
import ScrollToTop from './utils/scrollToTop'
import AboutMeSection from './components/aboutMeSection/AboutMeSection'
import LocationInfo from './components/locationInfo/LocationInfo'
import './styles/main.css'
import aboutMeData from './helpers/aboutMeData'
import services from './helpers/services'
import location from './helpers/location'
import AboutMeContainer from './сontainers/AboutMeContainer'

function App() {
	const orderNowRef = useRef(null)

	const [circles, setCircles] = useState([])

	// Функция для генерации кругов
	const generateCircles = num => {
		const newCircles = []
		for (let i = 0; i < num; i++) {
			const size = Math.random() * 200 + 100 // случайный размер от 100px до 300px
			const top = Math.random() * 100 + '%' // случайная позиция по вертикали
			const left = Math.random() * 100 + '%' // случайная позиция по горизонтали
			newCircles.push({ size, top, left })
		}
		setCircles(newCircles) // обновляем стейт с кругами
	}

	// Генерация кругов при монтировании компонента
	React.useEffect(() => {
		generateCircles(15) // генерируем 20 кругов
	}, [])

	const scrollToOrderNow = () => {
		if (orderNowRef.current) {
			orderNowRef.current.scrollIntoView({
				behavior: 'smooth',
				block: 'start',
			})
		}
	}

	return (
		<div className='App'>
			<ScrollToTop />
			<div className='content'>
				{circles.map((circle, index) => (
					<div
						key={index}
						className='circle'
						style={{
							top: circle.top,
							left: circle.left,
							width: circle.size + 'px',
							height: circle.size + 'px',
						}}
					></div>
				))}

				<div className='mb-8'>
					<ContactInfo
						phoneNumber={contactData.phoneNumber}
						address={contactData.address}
						buttonText={contactData.buttonText}
						buttonLink={contactData.buttonLink}
						logoSrc={contactData.logoSrc}
					/>
				</div>
				<div className='p-8'>
					<LashExtensionsPromo
						title={promoData.title}
						oldPrice={promoData.oldPrice}
						newPrice={promoData.newPrice}
						discountDescription={promoData.discountDescription}
						benefits={promoData.benefits}
					/>
				</div>

				<div ref={orderNowRef}>
					<OrderNow />
				</div>

				<AboutMeContainer scrollToOrderNow={scrollToOrderNow} />

				<div className='p-8'>
					<ServicesSection
						scrollToOrderNow={scrollToOrderNow}
						services={services}
					/>
				</div>

				<div className='pt-8 pl-8 pr-8 pb-2'>
					<LocationInfo location={location} />
				</div>
			</div>
		</div>
	)
}

export default App
