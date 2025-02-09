import React, { useState } from 'react'
import { motion } from 'framer-motion'
import ModalGallery from './../modalGallery/ModalGallery'

const ServiceItem = ({
	title,
	price,
	oldPrice,
	newPrice,
	mainImage,
	images,
	scrollToOrderNow,
	index,
}) => {
	const [isModalOpen, setIsModalOpen] = useState(false)
	const handleModalToggle = () => setIsModalOpen(!isModalOpen)

	return (
		<motion.div
			className='bg-white box-item border-2 border-gray-300 rounded-lg p-4 transform transition-all hover:scale-105 hover:shadow-lg cursor-pointer'
			whileInView={{ opacity: 1 }}
			initial={{ opacity: 0 }}
			viewport={{ once: true }}
			transition={{
				duration: 1.0,
				ease: 'easeOut',
				delay: index * 0.3,
			}}
		>
			<div className='w-full h-64 bg-gray-300 flex justify-center items-center rounded-lg'>
				{mainImage ? (
					<img
						src={mainImage}
						alt={title}
						className='w-full h-full object-cover rounded-lg'
					/>
				) : (
					<div className='w-full h-64 bg-gray-200'></div>
				)}
			</div>

			<div className='content mt-3 flex flex-col justify-between flex-grow'>
				<div className='serv-name text-center font-semibold text-lg'>
					{title}
				</div>
				<div
					className={`price-label text-center mt-2 ${oldPrice ? 'active' : ''}`}
				>
					<p className='text-lg sm:text-2xl md:text-2xl lg:text-2xl xl:text-2xl 2xl:text-2xl flex justify-center items-center'>
						{oldPrice && (
							<span className='line-through mr-2 text-[rgba(156,163,175,0.4)]'>
								{oldPrice} ₽
							</span>
						)}
						<span className='text-purple-700 text-2xl sm:text-2xl md:text-2xl lg:text-2xl xl:text-2xl 2xl:text-2xl font-bold'>
							{newPrice}{' '}
							<span className='text-2xl sm:text-2xl md:text-2xl lg:text-2xl xl:text-2xl 2xl:text-2xl'>
								₽
							</span>
						</span>
					</p>
				</div>
			</div>

			<div className='row justify-content-between center-row mt-4'>
				<div className='col-6 flex justify-start'>
					<button
						onClick={handleModalToggle}
						className='see-more__btn border-2 border-purple-600 text-purple-600 hover:bg-purple-600 hover:text-white font-medium px-6 py-2 rounded-full transition-all'
					>
						Фото работ
					</button>
				</div>
				<div className='col-6 flex justify-end'>
					<button
						onClick={() => {
							scrollToOrderNow()
						}}
						className='custom-btn bg-purple-600 text-white px-6 py-2 rounded-full hover:bg-purple-700 transition-all'
					>
						Записаться
					</button>
				</div>
			</div>

			{/* Модальное окно */}
			<ModalGallery
				images={images}
				isOpen={isModalOpen}
				onClose={handleModalToggle}
			/>
		</motion.div>
	)
}

export default ServiceItem
