import React from 'react'

const YandexMap = () => {
	return (
		<div>
			<h2>Местоположение работы</h2>
			<iframe
				src='https://maps.app.goo.gl/nspePPbbz5L8PeeB6'
				width='100%'
				height='400px'
				style={{ border: 0 }}
				title='Яндекс.Карта'
			></iframe>
		</div>
	)
}

export default YandexMap
