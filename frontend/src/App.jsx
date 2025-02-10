import React, { useRef, useState } from 'react'
import promoData from './helpers/promoData'
import LashExtensionsPromo from './components/lashExtensionsPromo/LashExtensionsPromo'
import OrderNow from './components/orderNow/OrderNow'
import ServicesSection from './components/servicesSection/ServicesSection'
import ScrollToTop from './utils/scrollToTop'
import LocationInfo from './components/locationInfo/LocationInfo'
import './styles/main.css'
import services from './helpers/services'
import AboutMeContainer from './сontainers/AboutMeContainer'
import AppDataProvider from './context/AppDataProvider'
import ContactInfoContainer from './сontainers/ContactInfoContainer'
import LocationInfoContainer from './сontainers/LocationInfoContainer'

function App() {
	const orderNowRef = useRef(null)

	const [circles, setCircles] = useState([])

	const generateCircles = num => {
		const newCircles = []
		for (let i = 0; i < num; i++) {
			const size = Math.random() * 200 + 100
			const top = Math.random() * 100 + '%'
			const left = Math.random() * 100 + '%'
			newCircles.push({ size, top, left })
		}
		setCircles(newCircles)
	}

	React.useEffect(() => {
		generateCircles(15)
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
		<AppDataProvider>
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

					<ContactInfoContainer />

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

					<LocationInfoContainer />
				</div>
			</div>
		</AppDataProvider>
	)
}

export default App
