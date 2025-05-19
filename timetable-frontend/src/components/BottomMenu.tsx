import React from 'react';

interface MenuItem {
  icon: string;
  label: string;
  active?: boolean;
}

export const BottomMenu: React.FC = () => {
  const menuItems: MenuItem[] = [
    { icon: '/src/assets/icons/schedule.svg', label: 'Расписание', active: true },
    { icon: '/src/assets/icons/groups.svg', label: 'Группы' },
    { icon: '/src/assets/icons/rute.svg', label: 'Маршрут' },
    { icon: '/src/assets/icons/news.svg', label: 'Новости' },
    { icon: '/src/assets/icons/settings.svg', label: 'Настройки' },
  ];

  return (
    <div className="fixed bottom-0 left-0 right-0 bg-white border-t border-gray-200">
      <div className="flex justify-around items-center h-16 px-2">
        {menuItems.map((item, index) => (
          <button
            key={index}
            className={`flex flex-col items-center justify-center flex-1 h-full ${
              item.active ? 'text-primary' : 'text-gray'
            }`}
          >
            <img
              src={item.icon}
              alt={item.label}
              className={`w-6 h-6 mb-1 ${item.active ? 'opacity-100' : 'opacity-60'}`}
            />
            <span className="text-xs font-inter">{item.label}</span>
          </button>
        ))}
      </div>
    </div>
  );
}; 