import React, { useState } from 'react'
import ReactDOM from 'react-dom'
import { motion } from 'framer-motion'
import './style.css'

const Notification = ({ message, type }) => {
	if (!message) return null

	const notificationVariants = {
		hidden: { opacity: 0, y: -50 },
		visible: { opacity: 1, y: 0 },
		exit: { opacity: 0, y: -50 },
	}

	return ReactDOM.createPortal(
		<motion.div
			className={`notification ${type === 'success' ? 'success' : 'error'}`}
			initial='hidden'
			animate='visible'
			exit='exit'
			variants={notificationVariants}
			transition={{ duration: 0.5, ease: 'easeOut' }}
		>
			<div className='font-bold text-2xl mb-2'>
				{type === 'success' ? 'Успешно!' : 'Ошибка'}
			</div>
			<p>{message}</p>
		</motion.div>,
		document.body
	)
}

const Modal = ({ isOpen, closeModal, submitForm, showNotification }) => {
	const [name, setName] = useState('')
	const [phone, setPhone] = useState('')

	if (!isOpen) return null

	const handleSubmit = e => {
		e.preventDefault()

		if (!name || !phone) {
			showNotification('Пожалуйста, заполните все поля.', 'error')
			return
		}

		submitForm(name, phone)
		showNotification('Ваш запрос успешно отправлен!', 'success')
		setName('')
		setPhone('')
		closeModal()
	}

	const modalVariants = {
		hidden: { opacity: 0, scale: 0.8, rotate: -10 },
		visible: { opacity: 1, scale: 1, rotate: 0 },
		exit: { opacity: 0, scale: 0.8, rotate: 10 },
	}

	return (
		<motion.div
			className='fixed inset-0 flex items-center justify-center bg-black bg-opacity-60 backdrop-blur-md z-50'
			initial='hidden'
			animate='visible'
			exit='exit'
			variants={modalVariants}
			transition={{ duration: 0.5, type: 'spring', stiffness: 100 }}
		>
			<motion.div
				className='bg-gradient-to-br from-purple-600/30 via-white/20 to-pink-500/30 backdrop-blur-md border border-white/20 p-6 rounded-2xl shadow-2xl max-w-md w-full text-center'
				variants={modalVariants}
			>
				<h3 className='text-2xl font-bold text-white mb-4'>Оставьте заявку</h3>
				<form onSubmit={handleSubmit} className='space-y-4'>
					<motion.input
						type='text'
						value={name}
						onChange={e => setName(e.target.value)}
						placeholder='Ваше имя'
						className='w-full p-3 text-white bg-white/10 rounded-lg border border-white/20 focus:ring-2 focus:ring-purple-400 outline-none transition duration-200'
						whileFocus={{ scale: 1.05 }}
					/>
					<motion.input
						type='tel'
						value={phone}
						onChange={e => setPhone(e.target.value)}
						placeholder='Ваш телефон'
						className='w-full p-3 text-white bg-white/10 rounded-lg border border-white/20 focus:ring-2 focus:ring-purple-400 outline-none transition duration-200'
						whileFocus={{ scale: 1.05 }}
					/>
					<div className='flex items-center justify-between'>
						<motion.button
							type='submit'
							className='px-6 py-3 bg-gradient-to-r from-purple-500 to-pink-500 text-white font-bold rounded-lg shadow-md hover:shadow-lg hover:from-purple-600 hover:to-pink-600 transition duration-200'
							whileHover={{ scale: 1.1 }}
						>
							Отправить
						</motion.button>
						<motion.button
							type='button'
							onClick={closeModal}
							className='px-6 py-3 bg-white/10 text-white font-bold rounded-lg hover:bg-white/20 transition duration-200'
							whileHover={{ scale: 1.1 }}
						>
							Закрыть
						</motion.button>
					</div>
				</form>
			</motion.div>
		</motion.div>
	)
}

const Phone = ({ phoneNumber, showNotification }) => {
	const [showToast, setShowToast] = useState(false)

	const copyToClipboard = () => {
		navigator.clipboard
			.writeText(phoneNumber)
			.then(() => {
				showNotification('Номер скопирован в буфер обмена!', 'success')
				setShowToast(true)
				setTimeout(() => {
					setShowToast(false)
				}, 3000)
			})
			.catch(err => {
				showNotification('Не удалось скопировать номер!', 'error')
				console.error('Не удалось скопировать номер: ', err)
			})
	}

	const isMobile = /iPhone|iPad|iPod|Android/i.test(navigator.userAgent)

	return (
		<motion.div
			className='phone'
			initial={{ opacity: 0, y: -50 }}
			animate={{ opacity: 1, y: 0 }}
			transition={{ duration: 0.5, ease: 'easeOut' }}
		>
			{isMobile ? (
				<a
					href={`tel:${phoneNumber}`}
					className='phone-link bg-purple-100 text-purple-600 font-bold text-xl rounded-lg px-8 py-4 hover:bg-purple-200 hover:text-purple-800 transition-all duration-200 shadow-lg w-full sm:max-w-xs'
				>
					{phoneNumber}
				</a>
			) : (
				<a
					href='#'
					className='phone-link bg-purple-100 text-purple-600 font-bold text-xl rounded-lg px-8 py-4 hover:bg-purple-200 hover:text-purple-800 transition-all duration-200 shadow-lg w-full sm:max-w-xs'
					onClick={e => {
						e.preventDefault()
						copyToClipboard()
					}}
					style={{
						whiteSpace: 'nowrap',
						overflow: 'hidden',
						textOverflow: 'ellipsis',
						display: 'block',
						width: '100%',
						maxWidth: '100%',
						overflowWrap: 'break-word',
						textAlign: 'center',
						wordBreak: 'break-word',
					}}
				>
					{phoneNumber}
				</a>
			)}
		</motion.div>
	)
}

const Address = ({ address }) => (
	<motion.div
		className='address bg-pink-100 text-pink-700 font-semibold text-lg rounded-lg px-8 py-4 mt-4 shadow-lg'
		initial={{ opacity: 0, y: 20 }}
		animate={{ opacity: 1, y: 0 }}
		transition={{ duration: 0.5 }}
	>
		{address}
	</motion.div>
)

const CallBackButton = ({ buttonText, handleClick }) => (
	<motion.div
		className='call-back-btn p-8'
		initial={{ opacity: 0, scale: 0.8 }}
		animate={{ opacity: 1, scale: 1 }}
		transition={{ duration: 0.6, type: 'spring', stiffness: 100 }}
	>
		<button
			onClick={handleClick}
			className='inline-flex items-center justify-center px-8 py-4 border-2 border-purple-600 rounded-2xl text-purple-600 font-semibold uppercase transition-all duration-200 hover:bg-purple-600 hover:text-white shadow-xl'
		>
			<span>{buttonText}</span>
		</button>
	</motion.div>
)

const ContactInfo = ({ phoneNumber, address }) => {
	const [showNotification, setShowNotification] = useState('')
	const [isModalOpen, setIsModalOpen] = useState(false)

	const handleButtonClick = () => {
		setIsModalOpen(true)
	}

	const closeModal = () => {
		setIsModalOpen(false)
	}

	const submitForm = (name, phone) => {
		console.log('Данные отправлены:', { name, phone })
	}

	const showNotificationMessage = (message, type) => {
		setShowNotification({ message, type })
		setTimeout(() => setShowNotification(''), 3000)
	}

	return (
		<div className='contact-info flex flex-col sm:flex-row justify-between items-center w-full sm:w-4/5 mx-auto mt-8'>
			<motion.div
				className='flex flex-col items-center justify-center w-full sm:w-1/3 space-y-8'
				initial={{ opacity: 0, y: -20 }}
				animate={{ opacity: 1, y: 0 }}
				transition={{ duration: 0.5, ease: 'easeOut' }}
			>
				<Phone
					phoneNumber={phoneNumber}
					showNotification={showNotificationMessage}
				/>
				<Address address={address} />
			</motion.div>

			<div className='flex flex-col items-center justify-center w-full sm:w-1/3'>
				<CallBackButton
					buttonText={'Заказать звонок'}
					handleClick={handleButtonClick}
				/>
			</div>

			<Modal
				isOpen={isModalOpen}
				closeModal={closeModal}
				submitForm={submitForm}
				showNotification={showNotificationMessage}
			/>

			<Notification
				message={showNotification.message}
				type={showNotification.type}
			/>
		</div>
	)
}

export default ContactInfo
