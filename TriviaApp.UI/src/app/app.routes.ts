import { Routes } from '@angular/router';
import { StartGame } from './start-game/start-game';
import { Question } from './question/question';
import { BaseLayout } from './layout/base-layout/base-layout';

export const routes: Routes = [{
  path: '',
  component: BaseLayout,
  children: [
    { path: '', component: StartGame },
    { path: 'quiz', component: Question },
  ]
},
{ path: '**', redirectTo: '', pathMatch: 'full' },
];
