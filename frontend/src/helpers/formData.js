// formData.js

export const formData = {
	availableDates: [
		new Date('2025-01-25'),
		new Date('2025-01-29'),
		new Date('2025-01-30'),
	],
	services: [
		'Классика',
		'Ламинирование',
		'2-D объем',
		'3-D объем',
		'4-D объем',
		'Голливудский объем',
	],
	getAvailableTimes: date => {
		if (date.toDateString() === new Date('2025-01-21').toDateString()) {
			return ['10:00', '12:00', '14:00']
		} else if (date.toDateString() === new Date('2025-01-22').toDateString()) {
			return ['11:00', '13:00', '15:00']
		} else {
			return ['09:00', '11:00', '13:00']
		}
	},
}
