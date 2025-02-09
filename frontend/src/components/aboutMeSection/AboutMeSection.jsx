import React, { useState } from 'react'
import { motion } from 'framer-motion'
import { useInView } from 'react-intersection-observer'
import ModalGallery from './../modalGallery/ModalGallery'

const AboutMeSection = ({ scrollToOrderNow, aboutMeData }) => {
	const { ref: sectionRef, inView } = useInView({
		triggerOnce: true,
		threshold: 0.2,
	})

	const [isModalOpen, setIsModalOpen] = useState(false)
	const handleModalToggle = () => setIsModalOpen(!isModalOpen)

	return (
		<>
			<ModalGallery
				images={aboutMeData.photogallery}
				isOpen={isModalOpen}
				onClose={handleModalToggle}
			/>

			<section
				ref={sectionRef}
				className='relative overflow-hidden py-20 bg-gradient-to-r from-pink-100 via-purple-50 to-blue-100'
			>
				{/* SVG-фон */}
				<div className='absolute inset-0'>
					<svg
						className='absolute top-0 left-0 w-96 h-96 opacity-20 animate-spin-slow'
						viewBox='0 0 200 200'
						xmlns='http://www.w3.org/2000/svg'
					>
						<path
							fill='#D1BBF8'
							d='M51.6,-66.2C63.9,-57.7,68.4,-37.7,65.9,-21.7C63.5,-5.7,54,-3.6,44.8,6.8C35.7,17.1,26.8,34.9,14.7,41.2C2.5,47.5,-13,42.2,-27.2,34.4C-41.4,26.5,-54.3,16,-59.6,1.8C-64.9,-12.5,-62.6,-31.5,-51.4,-40.6C-40.3,-49.8,-20.1,-49.1,-2.8,-46.9C14.6,-44.7,29.2,-41.1,51.6,-66.2Z'
							transform='translate(100 100)'
						/>
					</svg>
					<svg
						className='absolute bottom-0 right-0 w-80 h-80 opacity-30 animate-pulse'
						viewBox='0 0 200 200'
						xmlns='http://www.w3.org/2000/svg'
					>
						<path
							fill='#F8D1D1'
							d='M39.3,-57.7C52.8,-51.4,64.6,-40.4,66.8,-27.5C68.9,-14.5,61.5,-0.4,56.1,12.6C50.8,25.5,47.6,37.2,39.1,42.9C30.6,48.6,15.8,48.2,3.5,43.9C-8.8,39.6,-17.6,31.3,-28.3,24.2C-39,17,-51.6,11.2,-54.4,1.6C-57.2,-8,-50.3,-22.4,-40.1,-29.7C-30,-37,-15,-37.2,0.4,-37.6C15.8,-38,31.6,-38.6,39.3,-57.7Z'
							transform='translate(100 100)'
						/>
					</svg>
				</div>

				<div className='container mx-auto relative z-10 flex flex-wrap items-center'>
					{/* Левый блок с текстом */}
					<motion.div
						className={`w-full md:w-1/2 px-6 ${
							inView ? 'opacity-100' : 'opacity-0'
						}`}
						initial={{ opacity: 0, y: 50 }}
						animate={inView ? { opacity: 1, y: 0 } : {}}
						transition={{ duration: 0.8, ease: 'easeOut' }}
					>
						<h2 className='text-5xl font-extrabold text-gray-800 mb-6'>
							<span className='relative inline-block text-transparent bg-clip-text bg-gradient-to-r from-purple-600 to-pink-500'>
								{aboutMeData.title}
							</span>
						</h2>
						<p className='text-lg text-gray-700 mb-6 leading-relaxed'>
							{aboutMeData.description}
						</p>
						<div className='flex space-x-4'>
							<button
								onClick={handleModalToggle}
								className='px-6 py-3 bg-gradient-to-r from-purple-600 to-pink-500 text-white font-medium rounded-full shadow-lg hover:shadow-xl transform hover:scale-105 transition'
							>
								Фотогалерея
							</button>
							<button
								onClick={scrollToOrderNow}
								className='px-6 py-3 bg-gray-200 text-purple-600 font-medium rounded-full shadow-lg hover:bg-gray-100 transform hover:scale-105 transition'
							>
								Записаться
							</button>
						</div>
					</motion.div>

					{/* Правый блок с изображением */}
					<motion.div
						className={`w-full md:w-1/2 px-6 mt-8 sm:mt-6 sm:mb-6 relative ${
							inView ? 'opacity-100' : 'opacity-0'
						}`}
						initial={{ opacity: 0, x: 100 }}
						animate={inView ? { opacity: 1, x: 0 } : {}}
						transition={{ duration: 0.8, delay: 0.3, ease: 'easeOut' }}
					>
						<div className='group relative'>
							<img
								src={aboutMeData.image}
								alt='Тимофеева Юлия'
								className='w-full rounded-lg shadow-lg transform transition duration-300 group-hover:scale-105'
							/>
							<div className='absolute bottom-4 left-4 bg-white bg-opacity-80 p-4 rounded-lg shadow-md'>
								<h3 className='text-lg font-semibold text-gray-800'>
									{aboutMeData.fullName}
								</h3>
								<p className='text-sm text-gray-600'>{aboutMeData.job}</p>
							</div>
						</div>
					</motion.div>
				</div>
			</section>
		</>
	)
}

export default AboutMeSection
