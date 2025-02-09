import React, { useState, useEffect } from 'react'
import { motion } from 'framer-motion'

// Компонент блока акции
const DiscountBlock = ({ oldPrice, newPrice, description }) => (
	<div
		className='relative bg-gradient-to-r from-purple-100 to-pink-100 shadow-md rounded-xl p-6 flex flex-col items-center space-y-4'
		style={{ height: '250px', overflow: 'hidden' }} // Ограничиваем область видимости
	>
		<span className='bg-purple-600 text-white text-xs sm:text-sm font-bold px-4 py-1 rounded-full tracking-wider'>
			АКЦИЯ
		</span>

		<div className='text-center relative'>
			{/* Анимация полосы блика */}
			<motion.div
				className='absolute'
				initial={{ x: '-300%', rotate: '45deg' }}
				animate={{ x: '300%', rotate: '45deg' }}
				transition={{
					repeat: Infinity,
					duration: 5.0,
					repeatDelay: 2.0,
					ease: 'linear',
				}}
				style={{
					pointerEvents: 'none',
					position: 'absolute',
					top: 0,
					left: 0,
					width: '1000%',
					height: '100%',
					background:
						'linear-gradient(90deg, rgba(255, 255, 255, 0.2), rgba(255, 255, 255, 0.5))',
					zIndex: 10,
				}}
			/>

			{/* Перечеркнутая старая цена */}
			<p className='text-[rgba(156,163,175,0.4)] text-lg sm:text-xl md:text-2xl lg:text-3xl xl:text-2xl 2xl:text-3xl flex justify-center items-center'>
				<span className='line-through mr-1'>{oldPrice} ₽</span>
			</p>

			{/* Новая цена */}
			<p className='text-purple-700 text-3xl sm:text-5xl md:text-6xl lg:text-5xl xl:text-4xl 2xl:text-5xl font-bold flex items-center justify-center'>
				<span className='mr-1'>{newPrice}</span>
				<span className='text-3xl sm:text-5xl md:text-6xl lg:text-5xl xl:text-4xl 2xl:text-5xl'>
					₽
				</span>
			</p>
		</div>

		<p className='text-gray-600 text-xs sm:text-sm md:text-base'>
			{description}
		</p>
	</div>
)

// Компонент для списка преимуществ
const BenefitsList = ({ benefits }) => (
	<ul className='space-y-4'>
		{benefits.map((benefit, index) => (
			<li
				key={index}
				className='flex items-center bg-white shadow-md rounded-lg p-4 space-x-4 animate-fadeIn opacity-0 transform transition-opacity duration-500 delay-[calc(100ms*index)]'
				style={{ animationDelay: `${index * 150}ms` }}
			>
				{/* Галочка с меньшими размерами на больших экранах */}
				<div className='flex-shrink-0 w-6 h-6 sm:w-8 sm:h-8 md:w-9 md:h-9 lg:w-10 lg:h-10 flex items-center justify-center bg-gradient-to-r from-pink-500 to-purple-500 rounded-full text-white font-bold'>
					✓
				</div>
				<span className='text-gray-700 text-sm sm:text-lg'>{benefit}</span>
			</li>
		))}
	</ul>
)

// Основной компонент
const LashExtensionsPromo = ({
	title,
	oldPrice,
	newPrice,
	discountDescription,
	benefits,
}) => {
	// Состояние для обновления ключа компонента
	const [sectionKey, setSectionKey] = useState(Date.now())

	useEffect(() => {
		// Обработчик изменения размера окна
		const handleResize = () => {
			setSectionKey(Date.now()) // Меняем ключ, чтобы перерендерить компонент
		}

		// Добавляем обработчик события resize
		window.addEventListener('resize', handleResize)

		// Убираем обработчик при размонтировании компонента
		return () => {
			window.removeEventListener('resize', handleResize)
		}
	}, []) // Хук запускается только один раз при монтировании компонента

	return (
		<div
			key={sectionKey} // Обновляем ключ компонента при изменении размера окна
			className='flex flex-col items-center justify-center mx-auto max-w-4xl px-6 py-10 bg-gradient-to-b from-pink-50 via-purple-50 to-white rounded-xl shadow-lg space-y-8'
		>
			{/* Заголовок */}
			<h1 className='text-3xl sm:text-4xl font-extrabold text-transparent bg-clip-text bg-gradient-to-r from-purple-600 to-pink-500 text-center tracking-wider'>
				{title.toUpperCase()}
			</h1>

			{/* Блок акции */}
			<DiscountBlock
				oldPrice={oldPrice}
				newPrice={newPrice}
				description={discountDescription}
			/>

			{/* Список преимуществ */}
			<BenefitsList benefits={benefits} />
		</div>
	)
}

export default LashExtensionsPromo
