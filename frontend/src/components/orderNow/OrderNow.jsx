import React, { useState, useEffect } from 'react'
import ReactDOM from 'react-dom'
import DatePicker from 'react-datepicker'
import 'react-datepicker/dist/react-datepicker.css'
import { formData } from './../../helpers/formData' // Импортируем данные
import './style.css'

const OrderNow = () => {
	const [selectedDate, setSelectedDate] = useState(null)
	const [selectedTime, setSelectedTime] = useState('')
	const [availableTimes, setAvailableTimes] = useState([])
	const [showNotification, setShowNotification] = useState({
		message: '',
		type: '',
	})
	const [isLoading, setIsLoading] = useState(false)

	// Используем данные из formData.js
	const { availableDates, services, getAvailableTimes } = formData

	// Преобразуем даты в строковый формат (без учета времени)
	const availableDatesStr = availableDates.map(date => date.toDateString())

	// Функция для обработки изменения даты
	const handleDateChange = date => {
		setSelectedDate(date)

		// Получаем доступные времена для выбранной даты
		const times = getAvailableTimes(date)

		// Если доступны времена, то выбираем первое по умолчанию
		setAvailableTimes(times)
		setSelectedTime(times[0]) // по умолчанию выбираем первое время
	}

	// Заглушка для отправки формы
	const handleSubmit = async e => {
		e.preventDefault() // Предотвращаем стандартное поведение формы

		// Получаем значения из формы
		const name = e.target['your-name'].value
		const phone = e.target['phone'].value

		// Проверка на заполненность полей имени и телефона
		if (!name || !phone) {
			setShowNotification({
				message: 'Пожалуйста, заполните все обязательные поля (имя и телефон).',
				type: 'error',
			})
			setTimeout(() => setShowNotification({ message: '', type: '' }), 3000) // Убираем уведомление через 3 секунды
			return
		}

		// Проверка на заполненность даты и времени
		if (!selectedDate || !selectedTime) {
			setShowNotification({
				message: 'Пожалуйста, выберите дату и время.',
				type: 'error',
			})
			setTimeout(() => setShowNotification({ message: '', type: '' }), 3000) // Убираем уведомление через 3 секунды
			return
		}

		// Собираем данные формы
		const formData = {
			name: name,
			phone: phone,
			service: e.target['service'].value,
			date: selectedDate,
			time: selectedTime,
		}

		setIsLoading(true)

		// Вместо настоящего запроса, имитируем успех
		setTimeout(() => {
			// Замена на успешный ответ (по заглушке)
			setShowNotification({
				message: 'Ваш запрос принят! Ожидайте звонка для подтверждения.',
				type: 'success',
			})
			setTimeout(() => setShowNotification({ message: '', type: '' }), 3000) // Убираем уведомление через 3 секунды
			setIsLoading(false)
		}, 1500) // Имитируем задержку в 1.5 секунды
	}

	// Рендер уведомления с использованием React Portal
	const renderNotification = () => {
		if (!showNotification.message) return null

		return ReactDOM.createPortal(
			<div
				className={`notification ${
					showNotification.type === 'success' ? 'success' : 'error'
				}`}
			>
				<div className='font-bold text-2xl mb-2'>
					{showNotification.type === 'success' ? 'Успешно!' : 'Ошибка'}
				</div>
				<p>{showNotification.message}</p>
			</div>,
			document.body // Уведомление будет рендериться прямо в body
		)
	}

	// Отключение стандартной валидации браузера
	const handleInvalid = e => {
		e.preventDefault() // Отменяем стандартное поведение
		setShowNotification({
			message: 'Пожалуйста, заполните все обязательные поля.',
			type: 'error',
		})
		setTimeout(() => setShowNotification({ message: '', type: '' }), 3000) // Убираем уведомление через 3 секунды
	}

	// Перерендеривание компонента при изменении окна
	const [sectionKey, setSectionKey] = useState(Date.now())

	useEffect(() => {
		const handleResize = () => {
			setSectionKey(Date.now()) // Меняем ключ для принудительного рендеринга
		}

		window.addEventListener('resize', handleResize)

		// Очистка при размонтировании компонента
		return () => {
			window.removeEventListener('resize', handleResize)
		}
	}, [])

	return (
		<div id='ordernow' className='flex flex-col items-center py-8 p-6 fade-in'>
			<div className='subtitle text-3xl font-semibold text-gray-700 mb-6 text-center'>
				Запись на наращивание ресниц
			</div>

			{/* Обновляем key при изменении размера окна */}
			<div key={sectionKey}>
				{/* Форма без отправки данных на сервер */}
				<form
					onSubmit={handleSubmit}
					className='wpcf7-form w-full max-w-4xl bg-white p-8 rounded-xl shadow-lg'
					onInvalid={handleInvalid} // Отключаем стандартное уведомление
				>
					<div className='form-wrap space-y-6'>
						<div className='grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6'>
							{/* Имя */}
							<div className='w-full'>
								<input
									type='text'
									name='your-name'
									required
									className='w-full p-4 border-2 border-gray-300 rounded-md text-lg focus:outline-none focus:ring-2 focus:ring-[#9B6BE6]'
									placeholder='Имя'
								/>
							</div>

							{/* Номер телефона */}
							<div className='w-full'>
								<input
									type='text'
									name='phone'
									required
									className='w-full p-4 border-2 border-gray-300 rounded-md text-lg focus:outline-none focus:ring-2 focus:ring-[#9B6BE6]'
									placeholder='Номер телефона'
								/>
							</div>

							{/* Услуга */}
							<div className='w-full relative'>
								<select
									name='service'
									className='w-full p-4 h-16 border-2 border-gray-300 rounded-md text-lg focus:outline-none focus:ring-2 focus:ring-[#9B6BE6] appearance-none'
								>
									{services.map((service, index) => (
										<option key={index} value={service}>
											{service}
										</option>
									))}
								</select>
								{/* Стилизация для стрелки */}
								<div className='absolute top-1/2 right-4 transform -translate-y-1/2 pointer-events-none'>
									<svg
										className='w-6 h-6 text-gray-400'
										fill='none'
										stroke='currentColor'
										viewBox='0 0 24 24'
										xmlns='http://www.w3.org/2000/svg'
									>
										<path
											strokeLinecap='round'
											strokeLinejoin='round'
											strokeWidth={2}
											d='M19 9l-7 7-7-7'
										/>
									</svg>
								</div>
							</div>

							{/* Дата */}
							<div className='w-full'>
								<DatePicker
									selected={selectedDate}
									onChange={handleDateChange}
									placeholderText='Выберите дату'
									className='w-full p-4 border-2 border-gray-300 rounded-md text-lg focus:outline-none focus:ring-2 focus:ring-[#9B6BE6]'
									minDate={new Date()}
									filterDate={date => {
										const isAvailable = availableDatesStr.includes(
											date.toDateString()
										)
										const isPastDate = date < new Date()
										return isAvailable && !isPastDate
									}}
									dateFormat='dd/MM/yyyy'
									dayClassName={date =>
										availableDatesStr.includes(date.toDateString()) &&
										date >= new Date()
											? 'bg-[rgba(255,200,83,0.6)] text-black'
											: 'bg-[rgba(255,107,107,0.05)] text-black'
									}
								/>
							</div>

							{/* Время */}
							<div className='w-full'>
								{selectedTime && (
									<select
										value={selectedTime}
										onChange={e => setSelectedTime(e.target.value)}
										className='w-full p-4 border-2 border-gray-300 rounded-md text-lg focus:outline-none focus:ring-2 focus:ring-[#9B6BE6]'
									>
										{availableTimes.map((time, index) => (
											<option key={index} value={time}>
												{time}
											</option>
										))}
									</select>
								)}
							</div>
						</div>

						{/* Кнопка отправки */}
						<div className='mt-6'>
							<button
								type='submit'
								className='w-full py-3 bg-[#9B6BE6] text-white text-xl font-semibold rounded-lg hover:bg-[#7a51c1] focus:outline-none focus:ring-2 focus:ring-[#9B6BE6]'
								disabled={isLoading}
							>
								{isLoading ? 'Загрузка...' : 'Отправить'}
							</button>
						</div>
					</div>
				</form>

				{renderNotification()}
			</div>
		</div>
	)
}

export default OrderNow
