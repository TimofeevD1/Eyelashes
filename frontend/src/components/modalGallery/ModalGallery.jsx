import React, { useState, useRef, useEffect } from 'react'
import ReactDOM from 'react-dom'
import { motion } from 'framer-motion'
import { FaExpand, FaCompress } from 'react-icons/fa'
import './style.css'

const ModalGallery = ({ images, isOpen, onClose }) => {
	const [currentImageIndex, setCurrentImageIndex] = useState(0)
	const [isFullscreen, setIsFullscreen] = useState(false)

	const thumbnailsRef = useRef(null)
	const modalRef = useRef(null)

	useEffect(() => {
		if (thumbnailsRef.current) {
			const currentThumbnail = thumbnailsRef.current.children[currentImageIndex]
			if (currentThumbnail) {
				currentThumbnail.scrollIntoView({
					behavior: 'smooth',
					block: 'nearest',
					inline: 'center',
				})
			}
		}
	}, [currentImageIndex])

	useEffect(() => {
		const handleEsc = event => {
			if (event.key === 'Escape') {
				handleFullscreenToggle()
			}
		}

		window.addEventListener('keydown', handleEsc)

		return () => {
			window.removeEventListener('keydown', handleEsc)
		}
	}, [])

	useEffect(() => {
		const handleFullscreenChange = () => {
			if (document.fullscreenElement) {
				setIsFullscreen(true)
			} else {
				setIsFullscreen(false)
			}
		}

		document.addEventListener('fullscreenchange', handleFullscreenChange)

		return () => {
			document.removeEventListener('fullscreenchange', handleFullscreenChange)
		}
	}, [])

	if (!isOpen) return null

	const handlePrevImage = () => {
		setCurrentImageIndex(prevIndex =>
			prevIndex > 0 ? prevIndex - 1 : images.length - 1
		)
	}

	const handleNextImage = () => {
		setCurrentImageIndex(prevIndex =>
			prevIndex < images.length - 1 ? prevIndex + 1 : 0
		)
	}

	const handleFullscreenToggle = () => {
		if (isFullscreen) {
			// Выход из полноэкранного режима
			if (document.exitFullscreen) {
				document.exitFullscreen()
			} else if (document.mozCancelFullScreen) {
				document.mozCancelFullScreen()
			} else if (document.webkitExitFullscreen) {
				document.webkitExitFullscreen()
			} else if (document.msExitFullscreen) {
				document.msExitFullscreen()
			}
		} else {
			// Вход в полноэкранный режим
			if (modalRef.current) {
				const element = modalRef.current

				// Для мобильных устройств Safari: используем webkitRequestFullscreen
				if (element.webkitRequestFullscreen) {
					element.webkitRequestFullscreen()
				} else if (element.requestFullscreen) {
					element.requestFullscreen()
				} else if (element.mozRequestFullScreen) {
					element.mozRequestFullScreen()
				} else if (element.msRequestFullscreen) {
					element.msRequestFullscreen()
				}
			}
		}

		// После изменения состояния нужно обновить флаг полноэкранного режима
		setIsFullscreen(prevState => !prevState)
	}

	const modalVariants = {
		hidden: { opacity: 0, scale: 0.95 },
		visible: { opacity: 1, scale: 1 },
		exit: { opacity: 0, scale: 0.95 },
	}

	const imageVariants = {
		hidden: { opacity: 0 },
		visible: { opacity: 1 },
	}

	return ReactDOM.createPortal(
		<motion.div
			className='fixed inset-0 flex justify-center items-center z-50'
			style={{
				backdropFilter: 'blur(10px)',
				backgroundColor: 'rgba(0, 0, 0, 0.7)',
			}}
			initial='hidden'
			animate='visible'
			exit='exit'
			variants={modalVariants}
			transition={{ duration: 0.3 }}
		>
			<motion.div
				ref={modalRef}
				className='bg-gradient-to-br from-purple-600/30 via-white/20 to-pink-500/30 backdrop-blur-md border border-white/20 p-6 rounded-2xl shadow-2xl max-w-md w-full text-center'
				variants={modalVariants}
				style={{
					width: '90%',
					maxWidth: '800px',
					height: '80vh',
					maxHeight: '80vh',
					overflow: 'hidden',
					position: 'relative',
				}}
			>
				{/* Close button */}
				<button
					onClick={onClose}
					className='absolute top-3 right-3 flex items-center justify-center bg-transparent text-white hover:bg-white/10 active:bg-white/20 p-2 rounded-full transition-all duration-200 ease-in-out focus:outline-none focus:ring-0 focus:ring-transparent'
					style={{
						width: '40px',
						height: '40px',
						fontSize: '24px',
						cursor: 'pointer',
						zIndex: 1000,
						boxSizing: 'border-box',
					}}
					onMouseEnter={e => {
						e.target.style.transform = 'scale(1.1)'
					}}
					onMouseLeave={e => {
						e.target.style.transform = 'scale(1)'
					}}
				>
					<span className='font-bold text-lg'>×</span>
				</button>

				{/* Main image with fullscreen button */}
				<motion.div
					className='relative flex justify-center items-center'
					style={{ height: 'calc(100% - 100px)' }}
					variants={imageVariants}
				>
					{images.length > 0 ? (
						<>
							{/* Fullscreen button inside image container (now at left top) */}
							<button
								onClick={handleFullscreenToggle}
								className='absolute top-3 left-3 text-white bg-black/50 p-2 rounded-full'
								style={{
									fontSize: '24px',
									border: 'none',
								}}
							>
								{isFullscreen ? <FaCompress /> : <FaExpand />}
							</button>
							<motion.img
								src={images[currentImageIndex]}
								alt={`Gallery Image ${currentImageIndex + 1}`}
								className='rounded-lg object-contain transition-all duration-300'
								style={{
									width: isFullscreen ? '100vw' : '100%',
									height: isFullscreen ? '100vh' : '100%',
									maxHeight: isFullscreen ? '100vh' : '100%',
									maxWidth: isFullscreen ? '100vw' : '100%',
									objectFit: 'contain',
								}}
								variants={imageVariants}
							/>
						</>
					) : (
						<div className='w-full h-full bg-gray-300 flex justify-center items-center rounded-lg'>
							<span className='text-gray-500 text-xl'>Здесь пока пусто</span>
						</div>
					)}
				</motion.div>

				{/* Navigation buttons */}
				{images.length > 0 && (
					<>
						<button
							onClick={handlePrevImage}
							className='absolute left-4 top-1/2 transform -translate-y-1/2 bg-gradient-to-r from-purple-500 to-pink-500 text-white p-4 rounded-full hover:bg-gradient-to-r hover:from-purple-600 hover:to-pink-600 transition-all shadow-md'
						>
							&lt;
						</button>
						<button
							onClick={handleNextImage}
							className='absolute right-4 top-1/2 transform -translate-y-1/2 bg-gradient-to-r from-purple-500 to-pink-500 text-white p-4 rounded-full hover:bg-gradient-to-r hover:from-purple-600 hover:to-pink-600 transition-all shadow-md'
						>
							&gt;
						</button>
					</>
				)}

				{/* Thumbnails with custom scroll bar */}
				{images.length > 0 && !isFullscreen && (
					<div
						ref={thumbnailsRef}
						className='mt-4 flex justify-start gap-4 overflow-x-auto p-1 scrollbar-custom'
					>
						{images.map((image, index) => (
							<motion.img
								key={index}
								src={image}
								alt={`Thumbnail ${index + 1}`}
								className={`w-16 h-16 object-cover rounded-lg cursor-pointer border-2 ${
									currentImageIndex === index
										? 'border-purple-500'
										: 'border-transparent'
								}`}
								onClick={() => setCurrentImageIndex(index)}
								initial={{ opacity: 0 }}
								animate={{ opacity: 1 }}
								transition={{ duration: 0.2 }}
							/>
						))}
					</div>
				)}
			</motion.div>
		</motion.div>,
		document.body
	)
}

export default ModalGallery
